resource "kubernetes_manifest" "api_namespace" {
  manifest = yamldecode(file("${path.module}/deployment/api-namespace.yaml"))
}

resource "kubernetes_manifest" "api_secret" {
  manifest = yamldecode(file("${path.module}/deployment/api-secret.yaml"))
}

resource "kubernetes_manifest" "api_hpa" {
  manifest = yamldecode(file("${path.module}/deployment/api-hpa.yaml"))
}

resource "kubernetes_manifest" "api_deployment" {
  manifest = yamldecode(file("${path.module}/deployment/api-deployment.yaml"))
}

resource "kubernetes_manifest" "api_service" {
  manifest = yamldecode(file("${path.module}/deployment/api-service.yaml"))
}

