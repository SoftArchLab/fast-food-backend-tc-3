terraform {
  backend "s3" {
    bucket         = "s3-infra-podapi"
    key            = "terraform.tfstate"
    region         = "us-east-1"
    encrypt        = true
  }
}