# variable "rg-location" {
#   description = "Resource group location"
#   type        = string
# }

# variable "rg-name" {
#   description = "Resource group name"
#   type        = string
# }

# variable "naming-suffix" {
#   description = "Naming suffix"
#   type        = string
# }


# module "redis" {
#   source              = "kumarvna/redis/azurerm"
#   version             = "1.0.0"
#   redis_instance_name = "redis-${var.naming-suffix}"

#   create_resource_group = false
#   resource_group_name   = var.rg-name
#   location              = var.rg-location

#   redis_server_settings = {
#     demoredischache-shared = {
#       sku_name            = "Premium"
#       capacity            = 1
#       shard_count         = 1
#       zones               = ["1"]
#       enable_non_ssl_port = true
#     }
#   }

#   redis_configuration = {
#     maxmemory_reserved = 2
#     maxmemory_delta    = 2
#     maxmemory_policy   = "allkeys-lru"
#   }

#   patch_schedule = {
#     day_of_week    = "Saturday"
#     start_hour_utc = 10
#   }
# }
