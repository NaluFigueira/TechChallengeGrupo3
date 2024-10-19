#!/bin/bash

kubectl apply -f ./_k8s/namespaces/namespace.yml

#----------------------------------------------------------------------
#----------------------------MS Users----------------------------------

# Apply Secrets
kubectl apply -n tech-challenge -f ./_k8s/secrets/acr-secret.yml 

# Apply ConfigMaps
kubectl apply -n tech-challenge -f ./_k8s/configmaps/prometheus-configmap.yml 
kubectl apply -n tech-challenge -f ./_k8s/configmaps/grafana-configmap.yml 

# Apply Persistent Volume
kubectl apply -n tech-challenge -f ./users/k8s/pvs/db-pv.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/pvs/db-pv.yml 

# Apply Persistent Volume Claims
kubectl apply -n tech-challenge -f ./_k8s/pvcs/monitoring-pvc.yml 
kubectl apply -n tech-challenge -f ./_k8s/pvcs/rabbitmq-pvc.yml 
kubectl apply -n tech-challenge -f ./users/k8s/pvcs/db-pvc.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/pvcs/db-pvc.yml 

# Apply services
kubectl apply -n tech-challenge -f ./_k8s/services/grafana-service.yml 
kubectl apply -n tech-challenge -f ./_k8s/services/prometheus-service.yml 
kubectl apply -n tech-challenge -f ./_k8s/services/rabbitmq-service.yml 
kubectl apply -n tech-challenge -f ./users/k8s/services/db-service.yml 
kubectl apply -n tech-challenge -f ./users/k8s/services/api-service.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/services/db-service.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/services/api-service.yml 

# Apply deployments
kubectl apply -n tech-challenge -f ./_k8s/deployments/monitoring-deployment.yml 
kubectl apply -n tech-challenge -f ./_k8s/deployments/rabbitmq-deployment.yml 
kubectl apply -n tech-challenge -f ./users/k8s/deployments/db-deployment.yml 
kubectl apply -n tech-challenge -f ./users/k8s/deployments/api-deployment.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/deployments/db-deployment.yml 
kubectl apply -n tech-challenge -f ./contacts-command/k8s/deployments/api-deployment.yml 

#----------------------------------------------------------------------
#----------------------------------------------------------------------