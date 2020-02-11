extends Node2D

onready var path = []

var timer2 = 0
export var time_to_spawn_next  = 2

var timer3 = 0

var counter = 0

onready var player_start_pos = $Player.position

var enemy = preload( "res://Scenes/Enemy.tscn" )

func _ready():
	counter = $Enemies.get_child_count()
	for child_index in $Enemies.get_child_count():
		var child = $Enemies.get_child(child_index)
		#var prev_child
		#if child_index == 0: prev_child = $Enemies.get_child($Enemies.get_child_count()-1)
		#else : prev_child = $Enemies.get_child(child_index-1)
		var next_child
		if child_index == $Enemies.get_child_count()-1: next_child = $Enemies.get_child(0)
		else : next_child = $Enemies.get_child(child_index+1)
		
		path.append([ child.position, next_child.position, Vector2(0,0) ] )
		child.index = randi() % counter
		child.modul = counter
			#if child_index == get_child_count()-1:
				


func spawn_new_enemy(delta):
	timer2 += delta
	if timer2 < time_to_spawn_next: return 
	timer2 = 0
	
	var instance = enemy.instance()
	instance.position = Vector2(0,0)
	instance.modul    = counter
	instance.index    = randi()%counter
	$Enemies.call_deferred("add_child", instance)

func _process(delta): 
	spawn_new_enemy(delta)
	
	timer3 += delta
	$Label.text = "TIME : " + str(timer3)

func reset():
	$Player.position = player_start_pos

	for child_index in $Enemies.get_child_count():
		var child = $Enemies.get_child(child_index)
		if child_index > counter-1:
			child.reset_secure = true
			child.call_deferred("queue_free")
		else:
			child.position = path[child_index][0]
			child.dir      = path[child_index][1]
			child.index    = child_index
	#path =  [[$Player.dir, $Player.expected_dir, $Player.position]]
	
	timer3 = 0
	timer2 = 0
