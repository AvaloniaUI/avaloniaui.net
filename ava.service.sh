# Install .NET Core 3.1.
# According to https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#install-the-sdk 
wget https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-runtime-3.1

# Create /home/prepare.sh
sudo -i
cat > /home/prepare.sh <<EOF
mkdir -p /home/avaloniaui.net
mkdir -p /tmp/avaloniaui.net
chmod 777 /home/avaloniaui.net
chmod 777 /tmp/avaloniaui.net
EOF

# Create /home/reload.sh
cat > /home/reload.sh <<EOF
systemctl stop ava.service > /dev/null 2>&1
rm -r /home/avaloniaui.net/* > /dev/null 2>&1 || true
cp -r /tmp/avaloniaui.net/* /home/avaloniaui.net/
rm -r /tmp/avaloniaui.net/* > /dev/null 2>&1 || true
chmod 777 /etc/systemd/system/ava.service
chmod 777 /home/avaloniaui.net/AvaloniaUI.Net
systemctl daemon-reload
systemctl enable ava.service 2>&1
systemctl restart ava.service
EOF

# Register ava.service as a new systemd service.
cat > /etc/systemd/system/ava.service <<EOF
[Unit]
Description=AvaloniaUI .NET Core App

[Service]
WorkingDirectory=/home/avaloniaui.net
ExecStart=/home/avaloniaui.net/AvaloniaUI.Net
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
