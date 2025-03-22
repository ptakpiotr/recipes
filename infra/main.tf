terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.24.0"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription-id
  features {}
}

locals {
  naming_suffix = "${terraform.workspace}-recipes"
}

variable "subscription-id" {
  description = "AZ Subscription ID"
  type        = string
}


resource "azurerm_resource_group" "rg" {
  name     = "rg-${local.naming_suffix}"
  location = "polandcentral"
}

module "web" {
  source                     = "./modules/web"
  app-service-dotnet-version = "8.0"
  app-service-plan-tier      = "F1"
  naming-suffix              = local.naming_suffix
  rg-location                = azurerm_resource_group.rg.location
  rg-name                    = azurerm_resource_group.rg.name
}

module "db" {
  source        = "./modules/db"
  pg-version    = "14"
  naming-suffix = local.naming_suffix
  rg-location   = azurerm_resource_group.rg.location
  rg-name       = azurerm_resource_group.rg.name
}

# module "cache" {
#   source        = "./modules/cache"
#   naming-suffix = local.naming_suffix
#   rg-location   = azurerm_resource_group.rg.location
#   rg-name       = azurerm_resource_group.rg.name
# }
