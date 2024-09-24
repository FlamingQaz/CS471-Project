extends Node


var player_money = 1000 #Starting amount of money
var max_money: int = 9999 #Max money cap *may not be needed
var basic_tower_cost = 100
var advanced_tower_cost = 250
var selected_tower_type: String = ""
var preview_tower: Node2D = null

#Update UI when money changes
signal money_updated(current_money)

# Function to add money (ex. when an enemy is defeated)
func add_money(amount: int):
	player_money = min(player_money + amount, max_money)  
	emit_signal("money_updated", player_money)

# Function to spend money (ex. when a tower is purchased)
func spend_money(amount: int) -> bool:
	if player_money >= amount: # if money is greater than or = to amount...
		player_money -= amount # subtract amount from player money
		emit_signal("money_updated", player_money)
		return true 
	else:
		print("Not enough money!") #Replace with UI for actual feedback to user ***
		return false # player does not have more than or = to amount to purchase

#Function to check current balance
func get_money() ->int:
	return player_money
#Tower Purchasing
#Tower Cost: Define Cost for each tower
#Tower Selection: When user selects a tower, check if theres enough money
#Tower Placement: If user does not have enough $, deduct the appropriate amount and allow user to place tower on the map
#Tower Placement Logic: Handle placement of tower on game map**
#UI Update: Reflect new money balance and the placement of the tower in the game UI

#Function to select a tower to purchase
func select_tower(tower_type: String):
	selected_tower_type = tower_type
	#show_tower_preview(tower_type)

#Function to attempt to place selected tower
func attempt_to_place_tower():
	if selected_tower_type == "basic_tower":
		if spend_money(basic_tower_cost):
			print("Has enough money for a Basic Tower!")
			#place_tower("BasicTowerScene")
		else:
			print("Not enough money for a Basic Tower!")
	elif selected_tower_type == "advanced_tower":
		if spend_money(advanced_tower_cost):
			print("Has enough money for an Advanced Tower!")
			#place_tower("AdvancedTowerScene")
		else:
			print("Not enough money for an Advanced Tower!")

# Called when the node enters the scene tree for the first time.
func _ready():
	emit_signal("money_updated", player_money)
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
