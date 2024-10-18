#!/bin/bash

#----------------------------------------------------------------------
#----------------------------MS Users----------------------------------
# Remove Services
kubectl delete services sqlserver -n tech-challenge
kubectl delete services tech-challenge-users-api-service -n tech-challenge
kubectl delete services tech-challenge-users-grafana-service -n tech-challenge

# Apply deployments
kubectl delete deployment tech-challenge-users-db-deployment -n tech-challenge
kubectl delete deployment tech-challenge-users-api-deployment -n tech-challenge
kubectl delete deployment tech-challenge-users-monitoring-deployment -n tech-challenge
kubectl delete deployment tech-challenge-rabbitmq-deployment -n tech-challenge

# Apply Persistent Volume Claims
kubectl delete pvc tech-challenge-users-db-pvc -n tech-challenge
kubectl delete pvc tech-challenge-users-monitoring-pvc -n tech-challenge
kubectl delete pvc tech-challenge-rabbitmq-pvc -n tech-challenge

# Apply Secrets
kubectl delete secrets acr-secret -n tech-challenge

# Apply ConfigMaps
kubectl delete configmap prometheus-config -n tech-challenge
kubectl delete configmap grafana-config -n tech-challenge
#----------------------------------------------------------------------
#----------------------------------------------------------------------

kubectl delete namespace tech-challenge