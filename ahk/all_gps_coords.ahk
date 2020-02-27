#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;use it with stimberries on a phioma to get feces

F1::
	Reload
	
	
	

MouseGetPos, OutputVarX, OutputVarY, OutputVarWin, OutputVarControl, 1

;debugging purpose
;MsgBox %OutputVarX% %OutputVarY% %OutputVarWin% %OutputVarControl%

;Xres := 1280
;Yres := 720
Xres := 1920
Yres := 1080

Xpos_center := Xres / 2
Ypos_center := Yres / 2

;ok 500
sleep 1000

;MouseClick, X1, Xpos_center + 350 , Ypos_center - 200 , 1, 0

;ok 500
sleep 1000

MouseClick, left

;MsgBox 922 154


MButton::

	tmpFile =all_gps_se2.txt

	test := 0
	
	temps:=50
	
	;en permanence
	While(test<2){
		;delay in ms
		;Sleep, 200
		;Sleep, 10
		
		test := test + 1
		
		if(test = 20){
			;msgbox %test%
		}
		
		;clipboard
		;clipboard = %clipboard%`r`n
		
		tmpString =%clipboard%

		FileAppend, %tmpString%`r`n, %tmpFile%
		
		;MsgBox %clipboard%
		
		clipboard =   ; Empty the clipboard.
		
		sleep 200

		;1650*895
		MouseClick, X1, 1650 , 895 , 1, 0
		sleep 200
		Click, down
		sleep 200
		Click, up
		;MouseClick, X1, Xpos_center + 350 , Ypos_center - 200 , 1, 0
		
		sleep 200
		
		send {Down down}
		sleep 200
		send {Down up}
		sleep 200
		
		send {Up down}
		sleep 200
		send {Up up}
		sleep 200
		send {Up down}
		sleep 200
		send {Up up}
		sleep 200
		
		send {Down down}
		sleep 200
		send {Down up}
		sleep 200
		
		
	}

return