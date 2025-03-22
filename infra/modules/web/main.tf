variable "app-service-plan-tier" {
  description = "App service plan tier"
  type        = string
  default     = "F1"
}

variable "app-service-dotnet-version" {
  description = "Dotnet version"
  type        = string
  default     = "8.0"
}

variable "rg-location" {
  description = "Resource group location"
  type        = string
}

variable "rg-name" {
  description = "Resource group name"
  type        = string
}

variable "naming-suffix" {
  description = "Naming suffix"
  type        = string
}


resource "azurerm_service_plan" "app_plan" {
  name                = "app_plan-${var.naming-suffix}"
  location            = var.rg-location
  resource_group_name = var.rg-name
  os_type             = "Linux"
  sku_name            = var.app-service-plan-tier
}

resource "azurerm_linux_web_app" "app" {
  name                = "app-${var.naming-suffix}"
  location            = var.rg-location
  resource_group_name = var.rg-name
  service_plan_id     = azurerm_service_plan.app_plan.id
  depends_on          = [azurerm_service_plan.app_plan]
  https_only          = true
  site_config {
    minimum_tls_version = "1.2"
    always_on           = false
    application_stack {
      dotnet_version = var.app-service-dotnet-version
    }
  }
}
