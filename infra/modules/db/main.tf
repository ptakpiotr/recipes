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

variable "pg-version" {
  description = "Postgres version"
  type        = string
  default     = "14"
}

resource "azurerm_virtual_network" "vnet" {
  name                = "vnet-${var.naming-suffix}"
  location            = var.rg-location
  resource_group_name = var.rg-name
  address_space       = ["10.0.0.0/16"]
}

resource "azurerm_network_security_group" "nsg" {
  name                = "nsg-${var.naming-suffix}"
  location            = var.rg-location
  resource_group_name = var.rg-name

  security_rule {
    name                       = "nsgsecrule"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
  }
}

resource "azurerm_subnet" "subnet" {
  name                 = "subnet-${var.naming-suffix}"
  virtual_network_name = azurerm_virtual_network.vnet.name
  resource_group_name  = var.rg-name
  address_prefixes     = ["10.0.2.0/24"]
  service_endpoints    = ["Microsoft.Storage"]

  delegation {
    name = "fs"

    service_delegation {
      name = "Microsoft.DBforPostgreSQL/flexibleServers"

      actions = [
        "Microsoft.Network/virtualNetworks/subnets/join/action",
      ]
    }
  }
}

resource "azurerm_subnet_network_security_group_association" "subnet_security_group" {
  subnet_id                 = azurerm_subnet.subnet.id
  network_security_group_id = azurerm_network_security_group.nsg.id
}

resource "azurerm_private_dns_zone" "private_dns" {
  name                = "${var.naming-suffix}-pdz.postgres.database.azure.com"
  resource_group_name = var.rg-name

  depends_on = [azurerm_subnet_network_security_group_association.subnet_security_group]
}

resource "azurerm_private_dns_zone_virtual_network_link" "virtual_network_link" {
  name                  = "${var.naming-suffix}-pdzvnetlink.com"
  private_dns_zone_name = azurerm_private_dns_zone.private_dns.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
  resource_group_name   = var.rg-name
}

resource "random_password" "pass" {
  length = 20
}

resource "azurerm_postgresql_flexible_server" "pg" {
  name                          = "pg-${var.naming-suffix}"
  resource_group_name           = var.rg-name
  location                      = var.rg-location
  version                       = var.pg-version
  delegated_subnet_id           = azurerm_subnet.subnet.id
  private_dns_zone_id           = azurerm_private_dns_zone.private_dns.id
  administrator_login           = "dbadmin"
  administrator_password        = random_password.pass.result
  zone                          = "1"
  storage_mb                    = 32768
  sku_name                      = "GP_Standard_D2s_v3"
  backup_retention_days         = 7
  public_network_access_enabled = false

  depends_on = [azurerm_private_dns_zone_virtual_network_link.virtual_network_link]
}

resource "azurerm_postgresql_flexible_server_database" "db" {
  name      = "db-${var.naming-suffix}"
  server_id = azurerm_postgresql_flexible_server.pg.id
  collation = "en_US.utf8"
  charset   = "UTF8"
}

output "postgresql_flexible_server_admin_password" {
  sensitive = true
  value     = azurerm_postgresql_flexible_server.pg.administrator_password
}
