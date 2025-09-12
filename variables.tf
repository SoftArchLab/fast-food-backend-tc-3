variable "region" {
  default = "us-east-1"
  description = "AWS region"
}

variable "kubeconfig" {
  description = "Caminho para o arquivo kubeconfig"
  type        = string
  default     = "~/.kube/config"
}