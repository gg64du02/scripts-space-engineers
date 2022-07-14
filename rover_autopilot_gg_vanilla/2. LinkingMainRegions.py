
# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

import numpy as np

import os

from skimage import measure

import array as arr

# folderNameSource = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/EarthLike/"

folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"


# folderNameTarget = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/EarthLikeProcessed/"

if(os.path.exists(folderNameSource)==True):
    print(folderNameSource, "exists")
else:
    print(folderNameSource, "does not exist")

# if(os.path.exists(folderNameTarget)==True):
#     print(folderNameTarget, "exists")
# else:
#     print(folderNameTarget, "does not exist")

from pathlib import Path

# fileNamePath = folderName + "back.png"
#
# if(Path(fileNamePath).exists()==True):
#     print(fileNamePath, "exists")
# else:
#     print(fileNamePath, "does not exist")


# Pertam stock ?
# planet_radius = 30000 #in meters
center_of_planet = np.asarray([-3967231.5,-32231.5,-767231.5])
# testThisGPSnpArray = np.asarray([-3937194.48,-31541.38,-764329.95])


# files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
files = {"back_thres_abs_sobelxy_step1.png","down_thres_abs_sobelxy_step1.png","front_thres_abs_sobelxy_step1.png","left_thres_abs_sobelxy_step1.png","right_thres_abs_sobelxy_step1.png","up_thres_abs_sobelxy_step1.png"}

# files = {"back_thres_abs_sobelxy_step1.png"}

full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)
# exit()

def generated_gps_point_on_cube_function(pointPixel, faceNumber, planet_radius):

    intX = 0
    intY = 0
    intZ = 0

    # if (pointPixel == null){
    #     pointPixel =  new Point(0, 0);
    # }

    pointPixel = [(2 * planet_radius / 2048) * pointPixel[0], (2 * planet_radius / 2048) * pointPixel[1]];

    generated_gps_point_on_cube = [0, 0, 0]

    if (faceNumber == 0):
        intX = 1 * (- planet_radius+pointPixel[1] * 1);
        intY = -1 * (- planet_radius+pointPixel[0] * 1);
        # // intZ = planet_radius * (centroid_surface_lack[1]-2048 / 2) * planet_radius;
        generated_gps_point_on_cube = [intX, intY, planet_radius]

    if (faceNumber == 1):
        intX = 1 * (- planet_radius+pointPixel[1] * 1);
        # // intY = -1 * (- planet_radius+pointPixel[0] * 1);
        intZ = -1 * (- planet_radius+pointPixel[0] * 1);
        generated_gps_point_on_cube = [intX, -planet_radius, intZ]

    if (faceNumber == 2):
        intX = -1 * (- planet_radius+pointPixel[1] * 1);
        intY = -1 * (- planet_radius+pointPixel[0] * 1);
        # // intZ = planet_radius * (centroid_surface_lack[1]-2048 / 2) * planet_radius;
        generated_gps_point_on_cube = [intX, intY, -planet_radius]

    if (faceNumber == 3):
        # // intX = 1 * (- planet_radius+pointPixel[1] * 1);
        intY = -1 * (- planet_radius+pointPixel[0] * 1);
        intZ = -1 * (- planet_radius+pointPixel[1] * 1);
        generated_gps_point_on_cube = [planet_radius, intY, intZ]

    if (faceNumber == 4):
        # // intX = 1 * (- planet_radius+pointPixel[1] * 1);
        intY = -1 * (- planet_radius+pointPixel[0] * 1);
        intZ = 1 * (- planet_radius+pointPixel[1] * 1);
        generated_gps_point_on_cube = [-planet_radius, intY, intZ]

    if (faceNumber == 5):
        intX = -1 * (- planet_radius+pointPixel[1] * 1);
        # // intY = -1 * (- planet_radius+pointPixel[0] * 1);
        intZ = -1 * (- planet_radius+pointPixel[0] * 1);
        # // generated_gps_point_on_cube = arr.array('d', [intX, planet_radius, intZ, ]+center_of_planet);
        generated_gps_point_on_cube = [intX, planet_radius, intZ]

    generated_gps_point_on_cube = np.round(generated_gps_point_on_cube)

    return generated_gps_point_on_cube

def whichFaceIsIt(file_path):

    # // 0 is back
    # // 1 is down
    #
    # // 2 is front
    # // 3 is left
    #
    # // 4 is right
    # // 5 is up
    numbersFaces = [0,1,2,3,4,5]
    namesFaces = ["back","down", "front","left" , "right","up"]

    faceNumber, faceName = -1,"None"
    for faceIndex in range(0,len(numbersFaces)):
        # print(namesFaces[faceIndex])
        if(namesFaces[faceIndex] in file_path):
            print(namesFaces[faceIndex])
            faceNumber, faceName = faceIndex, namesFaces[faceIndex]

    return faceNumber,faceName

def convertArraryToGPSString(name,arrayOfThree):
    # GPS: eaDesert: 58189.34:-7111: -24526.78:  # FF75C9F1:
    tmpGpsString = "GPS: "+str(name)+ ":" + str(round(arrayOfThree[0],1)) + ":" + \
                   str(round(arrayOfThree[1],1)) + ":" + str(round(arrayOfThree[2],1)) + ":#F175DC:"
    return tmpGpsString

def isThisPointNearThisContour(pointToTest,contourToTestAgainst):

    for contourPoint in contourToTestAgainst:
        tmpPointContour = [contourPoint[0],contourPoint[1]]
        # aka l2 norm
        distanceEuclidian = np.linalg.norm(contourPoint-pointToTest)
        # print("distanceEuclidian:"+str(distanceEuclidian))
        if(distanceEuclidian<5):
            return True

    return False


planetRegionIndexFace = 0;

for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)

    img_inverted = np.invert(img)

    contours = measure.find_contours(img, 1)

    print("len(contours):+" + str(len(contours)))

    # 32* 64 = 2048
    # we don't need to test every pixel to detect regions
    for i in range(0,64):
        for j in range(0,64):

            # img_inverted[1, 1] = 0
            # img_inverted[2,2] = 127
            # # # possible values are 0 128 255

            if(img_inverted[i*32,j*32]==0):
                # print("pixel skipped")
                continue
                # break

            labeled = measure.label(img_inverted, background=False, connectivity=1)
            # labeled = measure.label(img_inverted, background=0, connectivity=1)
            # labeled = measure.label(img, background=False, connectivity=1)

            label = labeled[i*32, j*32]
            # label = labeled[j,j]
            # label = labeled[0, 0]

            # y,x
            # label = labeled[1245,91]

            rp = measure.regionprops(labeled)
            # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
            props = rp[label - 1]  # background is labeled 0, not in rp
            # props.bbox  # (min_row, min_col, max_row, max_col)
            # props.image  # array matching the bbox sub-image
            # print(len(props.coords))  # list of (row,col) pixel indices
            regionSize = len(props.coords)
            # print("regionSize", regionSize)
            # print("[j, j]",[j, j])

            # if(regionSize<10000):
            if(regionSize<52000):
                for point in props.coords:
                    img_inverted[point[0],point[1]] = 0;
                continue

            print("props.bbox:",props.bbox)

            # relevant infos for the TA auto pilot
            print("props.centroid_local:",props.centroid_local)
            planetRegionIndexFace += 1
            print("planetRegionIndexFace:",planetRegionIndexFace)

            # props.image[:, 0] vertical
            # props.image[0, :] horizontal
            # props.image[:,2047]

            # plt.imshow(img,cmap='gray')
            # # plt.imshow(img_inverted,cmap='gray')
            # plt.show()
            # img.close()
            # exit()
            # # img.delete()
            # # continue

            left_top = []
            left_bot = []
            right_top = []
            right_bot = []

            bot_right = []
            bot_left = []
            top_right = []
            top_left = []

            # print("img[0,0]:",img[0,0])
            # print("img[i,j]:",img[i,j])
            # print("img_inverted[0,0]:",img_inverted[0,0])
            # print("img_inverted[i,j]:",img_inverted[i,j])

            print("props.bbox:",props.bbox)
            # props.bbox: (1114, 0, 1405, 300)
            # props.bbox: (y_0, x_0, y_1, x_1)

            y_0 = props.bbox[0]
            x_0 = props.bbox[1]
            y_1 = props.bbox[2]
            x_1 = props.bbox[3]

            availiablePathRet = np.zeros((2048,2048))

            print("map:putting the coords into a map")

            tmpI = 0

            connectedCoords = props.coords
            for coord in connectedCoords:
                availiablePathRet[coord[0],coord[1]] = 1
                # tmpI=tmpI+1
                # if(tmpI%100000==0):
                #     print(tmpI," done")

            print("map:finished putting the coords into a")


            if(x_0==0):
                print("bounding box is on the left line")
                for tmpK in range(0,2048,1):
                    if(availiablePathRet[tmpK,0]==1):
                        if(left_top==[]):
                            left_top=[tmpK,0]
                for tmpK in range(2047,0,-1):
                    if(availiablePathRet[tmpK,0]==1):
                        if(left_bot==[]):
                            left_bot=[tmpK,0]

            if (x_1 == 2048):
                print("bounding box is on the right line")
                for tmpK in range(0, 2048, 1):
                    if (availiablePathRet[tmpK, 2047] == 1):
                        if (right_top == []):
                            right_top = [tmpK, 2047]
                for tmpK in range(2047, 0, -1):
                    if (availiablePathRet[tmpK, 2047] == 1):
                        if (right_bot == []):
                            right_bot = [tmpK, 2047]

            if(y_0==0):
                print("bounding box is on the top line")
                for tmpK in range(0, 2048, 1):
                    if (availiablePathRet[0, tmpK] == 1):
                        if (top_left == []):
                            top_left = [0, tmpK]
                for tmpK in range(2047,0,-1):
                    if(availiablePathRet[0, tmpK]==1):
                        if(top_right==[]):
                            top_right=[0, tmpK]

            if(y_1==2048):
                print("bounding box is on the bottom line")
                for tmpK in range(0, 2048, 1):
                    if (availiablePathRet[0, tmpK] == 1):
                        if (bot_left == []):
                            bot_left = [0, tmpK]
                for tmpK in range(2047,0,-1):
                    if(availiablePathRet[0, tmpK]==1):
                        if(bot_right==[]):
                            bot_right=[0, tmpK]


            print("left_top:",left_top)
            print("left_bot:",left_bot)
            print("right_top:",right_top)
            print("right_bot:",right_bot)

            print("bot_right:",bot_right)
            print("bot_left:",bot_left)
            print("top_right:",top_right)
            print("top_left:",top_left)

            # print(whichFaceIsIt(file_path))

            # points_to_tests_for_regions_bounds = [left_top,left_bot,
            #                    right_top,right_bot,
            #                    bot_right,bot_left,
            #                    top_right,top_left
            #                    ]
            points_to_tests_for_regions_bounds = [left_top,left_bot,
                               bot_left,bot_right,
                               right_bot,right_top,
                               top_right,top_left
                               # top_left,top_right
                               ]

            # print("points_to_tests_for_regions_bounds",points_to_tests_for_regions_bounds)
            # removing empty list for the point list
            # points_to_tests_for_regions_bounds=list(filter(lambda x: x, points_to_tests_for_regions_bounds))
            print("points_to_tests_for_regions_bounds",points_to_tests_for_regions_bounds)

            points_to_tests_for_regions_bounds_tmp = []

            for p in points_to_tests_for_regions_bounds:
                if(p not in points_to_tests_for_regions_bounds_tmp):
                    if([] != p):
                        points_to_tests_for_regions_bounds_tmp.append(p)

            # print("points_to_tests_for_regions_bounds_tmp",points_to_tests_for_regions_bounds_tmp)
            points_to_tests_for_regions_bounds = points_to_tests_for_regions_bounds_tmp.copy()
            print("points_to_tests_for_regions_bounds",points_to_tests_for_regions_bounds)

            region_contour_points_list = []
            points_to_test_in_region_contour = points_to_tests_for_regions_bounds.copy()

            move_on_to_next_region = False

            # result_polygon_region = [[]]
            # result_polygon_region = [[0,0]]
            # result_polygon_region = [[50,50]]
            result_polygon_region = [[50,50]]
            result_polygon_region = np.reshape(result_polygon_region,(1,2))

            contourTmp = contours.copy()
            contourTmpThatCanStillBeAdded = contours.copy()

            while (points_to_tests_for_regions_bounds):
                po = points_to_tests_for_regions_bounds[0]
                print("1len(points_to_tests_for_regions_bounds):" + str(len(points_to_tests_for_regions_bounds)))
                # print("1len(points_to_tests_for_regions_bounds_tmp):"+str(len(points_to_tests_for_regions_bounds_tmp)))
                isAnyPointCloseToAnyContour = False
                if (po != []):
                    while (contourTmp):
                        tmpContour = contourTmp[0]
                        if (len(tmpContour) > 2000):
                            # contour is an array of points (4k,2)
                            print("len(tmpContour):" + str(len(tmpContour)))
                            # isNear should be a bool
                            # print("po:"+str(po))
                            isNear = isThisPointNearThisContour(po, tmpContour)
                            # print("isNear:"+str(isNear))
                            if (isNear == True):
                                # print("if(isNear==True):")
                                # result_polygon_region = np.concatenate((result_polygon_region, tmpContour), axis=0)
                                isAnyPointCloseToAnyContour = True
                                break
                            else:
                                pass
                                # print("not if(isNear==True):")
                                # result_polygon_region = np.concatenate((result_polygon_region, np.reshape(po, (1, 2))), axis=0)
                        # if(isAnyPointCloseToAnyContour==True):
                        #     result_polygon_region = np.concatenate((result_polygon_region, tmpContour), axis=0)
                        contourTmp.remove(tmpContour)
                    # contourTmp = contours.copy()
                    contourTmp = contourTmpThatCanStillBeAdded
                points_to_tests_for_regions_bounds.remove(po)

                if (isAnyPointCloseToAnyContour == False):
                    # no contour are around, we keep the point
                    result_polygon_region = np.concatenate((result_polygon_region, np.reshape(po, (1, 2))), axis=0)
                else:
                    # one contour is around, we don't keep the point, we keep the contour
                    result_polygon_region = np.concatenate((result_polygon_region, tmpContour), axis=0)
                    contourTmpThatCanStillBeAdded.remove(tmpContour)


            if(np.array_equal(result_polygon_region[0],[50,50])==True):
                print("if(result_polygon_region[0]==[50,50]):")
                result_polygon_region[0]=result_polygon_region[len(result_polygon_region)-1]

            print("str(len(result_polygon_region)):"+str(len(result_polygon_region)))


            # # test_approx = measure.approximate_polygon(result_polygon_region,0.02)
            # # test_approx = measure.approximate_polygon(props.coords,0.02)
            # test_approx = measure.approximate_polygon(props.coords,2)

            # pass
            # # for test_p in test_approx:
            # #     print("test_p:"+test_p)
            # print("test_p:")
            # pass

            # exit()

            # check the type of contour and make a merge with the same type

            # for contour in contours:
            #     ax.plot(contour[:, 1], contour[:, 0], linewidth=1)
            #     # if()
            # for polyPoint in result_polygon_region:
            if(len(result_polygon_region)>2):
                # Display the image and plot all contours found
                fig, ax = plt.subplots()
                ax.imshow(img_inverted, cmap=plt.cm.gray)
                # # debug
                # result_polygon_region = points_to_tests_for_regions_bounds
                ax.plot(result_polygon_region[:,1], result_polygon_region[:,0], linewidth=5)
                # ax.plot(test_approx[:,1], test_approx[:,0], linewidth=1)

                ax.axis('image')
                ax.set_xticks([])
                ax.set_yticks([])
                plt.show()

            # exit()

            planet_radius = 30000

            faceNumber,faceName = whichFaceIsIt(file_path)

            print("faceNumber is:",faceNumber, " ,faceName is:",faceName)


            for point_to_convert in points_to_tests_for_regions_bounds:
                if(point_to_convert==[]):
                    continue
                # print("=================================")
                # print("point_to_convert:",point_to_convert)
                gen_ed_v3d = generated_gps_point_on_cube_function(point_to_convert,faceNumber,planet_radius)
                # print("gen_ed_v3d:",gen_ed_v3d)
                # print("" + str(faceNumber) + "_" + str(planetRegionIndexFace))
                # result_str ="" + str(faceNumber) + "_region_" + str(planetRegionIndexFace)+"_"+str(gen_ed_v3d[0])+","+str(gen_ed_v3d[1])+","+str(gen_ed_v3d[2])
                # print(result_str)
                gen_ed_v3d = gen_ed_v3d + center_of_planet
                name = str(faceNumber) + "_region_" + str(planetRegionIndexFace)
                print(""+convertArraryToGPSString(name, gen_ed_v3d))

            result_str_centroid = "" + str(faceNumber) + "_centroid_" + str(planetRegionIndexFace) + "_" + str( props.centroid_local[0]) + "," + str(props.centroid_local[1])
            print(result_str_centroid)

            # TODO: generate GPS to display them ingame to check the bounderies
            # TODO: should region linking be generated through the string list ? (ingame or in py ?)
            # ingame: upon script init
            # this would allow to temper and add "bridge" regions
            # py: making the generated list ?

            # cleaning the region
            for point in props.coords:
                img_inverted[point[0], point[1]] = 0;


            # TODO: generate data that can link region between faces

            # # possible values are 0 128 255
            # for m in range(0,2048):
            #     if(img[m,m]==128):
            #         print("[m,m]:",[m,m])
            #         print("img[m,m]:",img[m,m])

