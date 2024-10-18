#!/bin/bash

kubectl apply -f namespace.yml

#----------------------------------------------------------------------
#----------------------------MS Users----------------------------------

# Apply Secrets
kubectl apply -n tech-challenge -f ./users/k8s/secrets/acr-secret.yml 

# Apply ConfigMaps
kubectl apply -n tech-challenge -f ./users/k8s/configmaps/prometheus-configmap.yml 
kubectl apply -n tech-challenge -f ./users/k8s/configmaps/grafana-configmap.yml 

# Apply Persistent Volume Claims
kubectl apply -n tech-challenge -f ./users/k8s/pvcs/db-volume.yml 
kubectl apply -n tech-challenge -f ./users/k8s/pvcs/monitoring-volume.yml 

# Apply services
kubectl apply -n tech-challenge -f ./users/k8s/services/db-service.yml 
kubectl apply -n tech-challenge -f ./users/k8s/services/api-service.yml 
kubectl apply -n tech-challenge -f ./users/k8s/services/grafana-service.yml 

# Apply deployments
kubectl apply -n tech-challenge -f ./users/k8s/deployments/db-deployment.yml 
kubectl apply -n tech-challenge -f ./users/k8s/deployments/api-deployment.yml 
kubectl apply -n tech-challenge -f ./users/k8s/deployments/monitoring-deployment.yml 

#----------------------------------------------------------------------
#----------------------------------------------------------------------