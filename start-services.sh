#!/bin/bash
minikube service tech-challenge-users-api-service -n tech-challenge
minikube service tech-challenge-contacts-command-api-service -n tech-challenge
minikube service tech-challenge-contacts-query-api-service -n tech-challenge
minikube service tech-challenge-grafana-service -n tech-challenge
minikube service rabbitmq -n tech-challenge