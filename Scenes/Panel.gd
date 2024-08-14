extends Panel

var tower = preload("res://Enemies/Enemy_test_1.tscn")

func _on_gui_input(event):
	var tempTower = tower.instantiate()
	
	if event is InputEventMouseButton and event.button_mask == 0:
		
		tempTower.process_mode = Node.PROCESS_MODE_DISABLED
		var path = get_tree().get_root().get_node("Test Map 1/Towers")
		path.add_child(tempTower)
		tempTower.global_position = get_child(0).global_position
		
		queue_free()
