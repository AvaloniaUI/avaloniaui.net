# Install .NET Core 3.1.
# According to https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#install-the-sdk 
wget https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-runtime-3.1

# Register ava.service as a new systemd service.
sudo -i
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
