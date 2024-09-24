extends Panel

var tower = preload("res://Assets/Prefab Objects/Towers/test_tower.tscn")

@onready var playerCurrencyManager = $"../../Player/Currency Manager"

func _on_gui_input(event):
	var tempTower = tower.instantiate()
	
	if event is InputEventMouseButton and event.button_mask == 0:
		
		if playerCurrencyManager.spend_money(600) :
			var path = get_tree().get_root().get_node("Test Map 1/Towers")
			path.add_child(tempTower)
			tempTower.global_position = get_child(0).global_position
			
			queue_free()
