#
# import cv2 as cv
# import numpy as np
# from matplotlib import pyplot as plt
# files = {"back.png","front.png"}
# for file in files:
#     print("file:",file)
#     img = cv.imread(file,0)
#     plt.imshow(img)
#     # plt.imshow(img, cmap='gray')
#     plt.show()

# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt
files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
for file in files:
    print("file:",file)
    img = cv.imread(file,0)
    # plt.imshow(img)
    # if(bool(img)!=None):
    #     print("img is empty: exiting")
    #     exit()
    sobelx = cv.Sobel(img,cv.CV_64F,1,0,ksize=1)
    sobely = cv.Sobel(img,cv.CV_64F,0,1,ksize=1)
    sobelxy = np.add(sobelx , sobely)

    abs_sobelx = np.absolute(sobelx)
    abs_sobely = np.absolute(sobely)
    abs_sobelxy = np.add(abs_sobelx , abs_sobely)

    # plt.imshow(sobelxy,cmap='gray')
    plt.imshow(abs_sobelxy,cmap='gray')
    plt.show()
    # img.close()
    exit()
    # img.delete()