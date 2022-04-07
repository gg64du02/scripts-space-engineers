
# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt


files = {"back_thres_abs_sobelxy.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
for file in files:
    print("file:",file)

    img = cv.imread(file,0)

    # debugging purpose
    # display image before modifying
    # plt.imshow(img,cmap='gray')
    ## plt.imshow(img)
    # plt.show()

    for i in range(0,2048):
        for j in range(0,2048):
            if( i % 32==0):
                if( j % 32==0):

                    # vertical lines
                    # print("i",i,"j",j)
                    uninterruptedLine = True
                    for pixels in range(0,32):
                        if(img[i+pixels][j]==128):
                            uninterruptedLine = False
                    if(uninterruptedLine == True):
                        # cv.line(img,(i,j),(i+32,j),(255,255,255))
                        # bug? for some reason cv.line switch the axis ?
                        cv.line(img,(j,i),(j,i+32),(255,255,255))
                    # print()



                    # horizontal lines
                    # print("i",i,"j",j)
                    uninterruptedLine = True
                    for pixels in range(0,32):
                        if(img[i][j+pixels]==128):
                            uninterruptedLine = False
                    if(uninterruptedLine == True):
                        # bug? for some reason cv.line switch the axis ?
                        cv.line(img,(j,i),(j+32,i),(255,255,255))
                    # print()





    plt.imshow(img,cmap='gray')
    ## plt.imshow(img)
    plt.show()