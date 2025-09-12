# Outputs opcionais para exibir informações dos recursos
output "api_service_name" {
  value = kubernetes_manifest.api_service.manifest["metadata"]["name"]
}

output "mysql_service_name" {
  value = kubernetes_manifest.mysql_service.manifest["metadata"]["name"]
}
