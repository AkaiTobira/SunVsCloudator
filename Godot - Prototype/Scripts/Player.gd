extends KinematicBody2D


var dir          = Vector2(0, -1)
var expected_dir = Vector2(0, -1)
export var SPEED = 100
var turn_speed = 100
 
var pressed;
var dir_multipler = 1

func _ready():
	pass

func _input(event):
	if event is InputEventMouseButton:
		pressed       = event.is_pressed()
		expected_dir = (event.position - position).normalized()

func update_direction(delta):
	if pressed : expected_dir = (get_global_mouse_position() - position).normalized()
	rotation_degrees += dir_multipler * turn_speed * delta
	dir = (dir + (expected_dir - dir) * SPEED  * delta).normalized()

	
func _physics_process(delta):
	update_direction(delta)
	move_and_collide(dir * SPEED * delta)
	teleport_througt_screen()

func teleport_througt_screen():
	if position.x < 0   : position.x = 600
	if position.x > 600 : position.x = 0
	if position.y < 0   : position.y = 800
	if position.y > 800 : position.y = 0
