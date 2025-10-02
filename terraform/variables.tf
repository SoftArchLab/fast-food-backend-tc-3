variable "project_name" {
  default     = "fastfood"
  description = "Project name"
}

variable "eks_cluster_name" {
  default     = "EKS-fastfood"
  description = "Project name"
}

variable "region" {
  default     = "us-east-1"
  description = "AWS region"
}

variable "vpc_cidr" {
  default = "172.31.0.0/16"
}

variable "public_subnets" {
  default = ["10.0.1.0/24", "10.0.2.0/24"]
}

variable "private_subnets" {
  default = ["10.0.3.0/24", "10.0.4.0/24"]
}

variable "labRole" {
  default = "arn:aws:iam::939530529281:role/LabRole"
}

variable "principalArn" {
  default = "arn:aws:iam::939530529281:role/voclabs"
}