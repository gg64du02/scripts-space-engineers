
# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

from skimage import measure


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

    countingLinks = 0
    for i in range(0,2048):
        for j in range(0,2048):
            if( i % 32==0):
                if( j % 32==0):

                    pass

                    if(img[i][j]==0):
                        continue

                    labeled = measure.label(img, background=False, connectivity=1)

                    label = labeled[i, j]

                    rp = measure.regionprops(labeled)
                    # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
                    props = rp[label - 1]  # background is labeled 0, not in rp
                    # props.bbox  # (min_row, min_col, max_row, max_col)
                    # props.image  # array matching the bbox sub-image
                    # print(len(props.coords))  # list of (row,col) pixel indices
                    regionSize = len(props.coords)

                    if(regionSize!=1):
                        print("regionSize", regionSize)
                        print("[i, j]",i,",", j)

                    # # print("i",i,"j",j)
                    # uninterruptedLine = True
                    # for pixels in range(0,32):
                    #     if(img[i+pixels][j]==128):
                    #         uninterruptedLine = False
                    # if(uninterruptedLine == True):
                    #     # cv.line(img,(i,j),(i+32,j),(255,255,255))
                    #     # bug? for some reason cv.line switch the axis ?
                    #     cv.line(img,(j,i),(j,i+32),(255,255,255))
                    #     countingLinks+=1
                    # # print()
                    #
                    #
                    #
                    # # horizontal lines
                    # # print("i",i,"j",j)
                    # uninterruptedLine = True
                    # for pixels in range(0,32):
                    #     if(img[i][j+pixels]==128):
                    #         uninterruptedLine = False
                    # if(uninterruptedLine == True):
                    #     # bug? for some reason cv.line switch the axis ?
                    #     cv.line(img,(j,i),(j+32,i),(255,255,255))
                    #     countingLinks+=1
                    # # print()
    print("countingLinks",countingLinks)




    # plt.imshow(img,cmap='gray')
    # ## plt.imshow(img)
    # plt.show()