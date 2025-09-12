# Aplica todos os manifests YAML do Kubernetes usando o provider Kubernetes
# Certifique-se de que o provider está configurado corretamente em main.tf

resource "kubernetes_manifest" "api_deployment" {
  manifest = yamldecode(file("${path.module}/api-deployment.yaml"))
}

resource "kubernetes_manifest" "api_hpa" {
  manifest = yamldecode(file("${path.module}/api-hpa.yaml"))
}

resource "kubernetes_manifest" "api_secret" {
  manifest = yamldecode(file("${path.module}/api-secret.yaml"))
}

resource "kubernetes_manifest" "api_service" {
  manifest = yamldecode(file("${path.module}/api-service.yaml"))
}

resource "kubernetes_manifest" "mysql_configmap" {
  manifest = yamldecode(file("${path.module}/mysql-configmap.yaml"))
}

resource "kubernetes_manifest" "mysql_deployment" {
  manifest = yamldecode(file("${path.module}/mysql-deployment.yaml"))
}

resource "kubernetes_manifest" "mysql_pvc" {
  manifest = yamldecode(file("${path.module}/mysql-pvc.yaml"))
}

resource "kubernetes_manifest" "mysql_secret" {
  manifest = yamldecode(file("${path.module}/mysql-secret.yaml"))
}

resource "kubernetes_manifest" "mysql_service" {
  manifest = yamldecode(file("${path.module}/mysql-service.yaml"))
}
