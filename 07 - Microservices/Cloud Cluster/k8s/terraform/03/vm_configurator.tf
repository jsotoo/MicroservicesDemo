# Creamos una máquina virtual
# https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/linux_virtual_machine
#resource "azurerm_linux_virtual_machine" "vmConfigurator" {    

resource "azurerm_virtual_machine" "vmConfigurator" {    
    count                   = length(var.vm_configurator_config)
    name                    = "${var.vm_configurator_config[count.index].name}"
    resource_group_name     = azurerm_resource_group.rg.name
    location                = azurerm_resource_group.rg.location
    #size                   = var.vm_size
    vm_size                 = var.vm_size
    #admin_username         = var.admin_user.username
    #admin_password         = var.admin_user.password
    network_interface_ids   = [ azurerm_network_interface.myNic[index(azurerm_network_interface.myNic.*.name, "vmnic-${var.vm_configurator_config[count.index].name}-${var.sufix}")].id ]
    #disable_password_authentication = false

    os_profile {
        computer_name = "${var.vm_configurator_config[count.index].name}"
        admin_username = var.admin_user.username
        admin_password = var.admin_user.password
    }

    os_profile_linux_config {
        disable_password_authentication = true
	    ssh_keys {
           path = "/home/${var.admin_user.username}/.ssh/authorized_keys"
		   key_data = "${file("id_rsa.pub")}" # you need to ahve this SSH key file somewhere it can be picked up and sent
        }		
    }

    storage_os_disk {
        name = "${var.vm_configurator_config[count.index].name}_OSDisk"
        caching = "ReadWrite"
        create_option = "FromImage"
        managed_disk_type = "${var.vm_configurator_config[count.index].osDisk.type}"
        #storage_account_type = "Standard_LRS"
    }

    plan {
        name      = "centos-8-stream-free"
        product   = "centos-8-stream-free"
        publisher = "cognosys"
    }

    storage_image_reference {
        publisher   = "cognosys"
        offer       = "centos-8-stream-free"
        sku         = "centos-8-stream-free"
        version     = "1.2019.0810"
    }

    provisioner "file" {
        source      = "id_rsa"
        destination = "~/.ssh/id_rsa"
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }

    provisioner "file" {
        source      = "id_rsa.pub"
        destination = "~/.ssh/id_rsa.pub"
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }
    
	provisioner "remote-exec" {
        inline = [
			"mkdir ansible",
			"mkdir apps"
        ]
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }
	
	provisioner "file" {
        source      = "../../ansible/"
        destination = " /home/adminUser/ansible/"
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }
	
	provisioner "file" {
        source      = "../../../apps/"
        destination = " /home/adminUser/apps/"
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }

	provisioner "file" {
        source      = "configure-vm.sh"
        destination = "configure-vm.sh"
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }
	
    provisioner "remote-exec" {
        inline = [
            "sudo chmod +x configure-vm.sh",
            "sh configure-vm.sh '${var.vm_configurator_config[count.index].ip}' '${var.vm_configurator_config[count.index].name}' '${var.vm_configurator_config[count.index].type}' '${var.admin_user.username}' '${var.admin_user.password}'"
        ]
        connection {
            type = "ssh"
            user = var.admin_user.username
            private_key = "${file("id_rsa")}"
            host = "${azurerm_public_ip.myPublicIp[index(azurerm_public_ip.myPublicIp.*.name, "vmip-${var.vm_configurator_config[count.index].name}-${var.sufix}-PublicIP")].domain_name_label}.westeurope.cloudapp.azure.com"
        }
    }

    tags = {
        environment = "Microservices"
    }

}
