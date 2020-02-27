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
	
	fileread, contents,  %tmpFile%	;reads the notepad
	
	temps:=50

	loop, parse, contents, `n	;read notepad line by line
	{
		;if a_index = 3	;if line number = 3
			;line%a_index% = %a_loopfield%	;assign var
		clipboard = %a_loopfield%
		
		MouseClick, X1, 464 , 933 , 1, 0
		sleep %temps%
		Click, down
		sleep %temps%
		Click, up
		sleep %temps%
	}
exitapp

	
loop, parse, contents, `n	;read notepad line by line
{
	if a_index = 3	;if line number = 3
		line%a_index% = %a_loopfield%	;assign var
}

msgbox %line3%

;#######################################################################
exitapp
