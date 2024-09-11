extends Node2D


@onready var path = preload("res://Assets/Prefab Objects/Map Systems/Path_1.tscn")
@onready var count = 1;
@onready var wave = 1;
@onready var wave1Count = 5
@onready var wave2Count = 10
@onready var wave3Count = 15

func _on_ready():
	get_child(0).start(5)
	pass # Replace with function body.


func _on_timer_timeout():
	
	if(count == 1):
		get_child(0).start(1)
		
	elif(count == wave1Count && wave == 1):
		wave = 2
		count = 0
		get_child(0).start(10)
	elif(count == wave2Count && wave == 2):
		wave = 3
		count = 0
		get_child(0).start(10)
	elif(count == wave3Count && wave == 3):
		get_child(0).queue_free()
		pass
	else:
		get_child(0).start(1)
		pass	
	
	count = count + 1	
	var tempPath = path.instantiate()
	add_child(tempPath)
	pass
