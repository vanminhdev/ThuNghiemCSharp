#!/bin/bash
set -e

# Append custom configuration to rabbitmq.conf
echo "loopback_users.guest = false" >> /etc/rabbitmq/rabbitmq.conf

# Run the original entrypoint of RabbitMQ
exec docker-entrypoint.sh rabbitmq-server
