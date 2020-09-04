# Install .NET Core 3.1.
# According to https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#install-the-sdk 
wget https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-runtime-3.1 aspnetcore-runtime-3.1

# Prepare the 'azure' user.
sudo -i
adduser --system --disabled-password --shell /bin/bash --home /home/azure azure
echo "azure ALL= NOPASSWD: /bin/systemctl stop ava.service, /bin/systemctl restart ava.service" >> /etc/sudoers

# Prepare the directories.
mkdir -p /home/azure/avaloniaui.net
mkdir -p /tmp/avaloniaui.net
chmod 777 /home/azure/avaloniaui.net
chmod 777 /tmp/avaloniaui.net

# Create /home/reload.sh
cat > /home/azure/reload.sh <<EOF
sudo systemctl stop ava.service > /dev/null 2>&1
rm -r /home/azure/avaloniaui.net/* > /dev/null 2>&1 || true
cp -r /tmp/avaloniaui.net/* /home/azure/avaloniaui.net/
rm -r /tmp/avaloniaui.net/* > /dev/null 2>&1 || true
chmod 777 /home/azure/avaloniaui.net/AvaloniaUI.Net
sudo systemctl restart ava.service
EOF
chmod 777 /home/azure/reload.sh

# Register ava.service as a new systemd service.
cat > /etc/systemd/system/ava.service <<EOF
[Unit]
Description=AvaloniaUI .NET Core App
[Service]
WorkingDirectory=/home/azure/avaloniaui.net
ExecStart=/home/azure/avaloniaui.net/AvaloniaUI.Net
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-ava
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=ASPNETCORE_URLS="http://*:80"
[Install]
WantedBy=multi-user.target
EOF

# Enable the new systemd service.
systemctl daemon-reload
systemctl enable ava.service 2>&1

# Generate SSH keys.
su azure
ssh-keygen -t rsa -P "" -f /home/azure/azure
mkdir -p /home/azure/.ssh
chmod -R 700 /home/azure/.ssh
touch /home/azure/.ssh/authorized_keys
cp -f /home/azure/azure.pub /home/azure/.ssh/authorized_keys
sed -i.old '1s;^;no-port-forwarding,no-x11-forwarding,no-agent-forwarding ;' /home/azure/.ssh/authorized_keys
echo "Add this key to your Azure service connection."
cat /home/azure/azure
