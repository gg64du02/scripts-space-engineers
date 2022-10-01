import pickle

import numpy as np

from matplotlib import pyplot as plt

from sklearn.neighbors import KDTree


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"


# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
files = {"back_sobelBitmap_64_min.pickle"}
# files = {"back_sobelBitmap_64_min.pickle","down_sobelBitmap_64_min.pickle","front_sobelBitmap_64_min.pickle","left_sobelBitmap_64_min.pickle","right_sobelBitmap_64_min.pickle","up_sobelBitmap_64_min.pickle"}

# sobelBitmap_64_min_TargetFilePath = stringTmpSplitted + "_sobelBitmap_64_min.pickle"


def numberOfStrFace(fileName):
    # // 0 is back
    # // 1 is down
    #
    # // 2 is front
    # // 3 is left
    #
    # // 4 is right
    # // 5 is up
    if(fileName == "back"):
        return 0
    if(fileName == "down"):
        return 1

    if(fileName == "front"):
        return 2
    if(fileName == "left"):
        return 3

    if(fileName == "right"):
        return 4
    if(fileName == "up"):
        return 5

    return -1

def conv2dTo3D(point, faceNumber):
    generated_gps_point_on_cube = []
    point = [point[0]*PR/1024,point[1]*PR/1024]
    if(faceNumber==0):
        intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        # //intZ = PR * (centroid_surface_lack[1]-2048/2) * PR
        generated_gps_point_on_cube = [intX, intY,PR]

    if(faceNumber==1):
        intX = 1*(- PR+point[1]*1)
        # //intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[0]*1)
        generated_gps_point_on_cube = [intX,-PR, intZ]

    if(faceNumber==2):
        intX = -1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        # //intZ = PR * (centroid_surface_lack[1]-2048/2) * PR
        generated_gps_point_on_cube = [intX, intY,-PR]

    if(faceNumber==3):
        # // intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[1]*1)
        generated_gps_point_on_cube = [PR,intY, intZ]

    if(faceNumber==4):
        # //intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        intZ = 1*(- PR+point[1]*1)
        generated_gps_point_on_cube = [-PR,intY, intZ]

    if(faceNumber==5):
        intX = -1*(- PR+point[1]*1)
        # // intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[0]*1)
        # //generated_gps_point_on_cube = arr.array('d', [intX,PR, intZ,]+center_of_planet)
        generated_gps_point_on_cube = [intX,PR, intZ]

    return generated_gps_point_on_cube
    # pass
    #
    # return [0,0,0]

full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)

# X = [[0,0,0]]
X = []

planet_radius = PR = 30000



for file_path in full_files_path:

    with open(file_path,'rb') as f:
        img = pickle.load(f)

    stringTmpSplitted = file_path.split("/")[-1:][0].split('_')[0]
    # .split('_')[0]
    print("stringTmpSplitted:",stringTmpSplitted)

    intFaceNumber = numberOfStrFace(stringTmpSplitted)
    print("intFaceNumber:",intFaceNumber)

    # plt.imshow(img)
    # plt.show()

    # converting 2d to 3d point:
    for x in range(0,2048):
        for y in range(0,2048):
            if(img[x,y]==128):
                pass
                # debug
                # X.append([0,x,y])

                pointIn3D = conv2dTo3D([x,y], intFaceNumber)
                # print("pointIn3D",pointIn3D)
                # TODO:faire la transformation en sphere ?
                l2norm = np.linalg.norm(pointIn3D,2)
                pointIn3D = pointIn3D / l2norm
                X.append(pointIn3D)

                pass


# appending to the planets points
# X is going to be the planets points
print("number of points",len(X))
tree = KDTree(X, leaf_size=2)
# dist, ind = tree.query(X[:1], k=3)
dist, ind = tree.query([[500,500,PR]], k=5)
print(ind)  # indices of 5 closest neighbors
print(dist)  # distances to 5 closest neighbors

xs = []
ys = []
zs = []
for t3dPoint in range(len(X)):
    xs.append(X[t3dPoint][0])
    ys.append(X[t3dPoint][1])
    zs.append(X[t3dPoint][2])

import matplotlib.pyplot as plt

fig = plt.figure(figsize=(12,7))
ax = fig.add_subplot(projection='3d')
# img = ax.scatter(xs, ys, zs,s=1)
img = ax.scatter(xs, ys, zs,s=0.2)
fig.colorbar(img)

plt.show()
pass


VoronoiAccumulatorSphere = VAS = []

# getting the radius without obstacles for each point
for file_path in full_files_path:

    with open(file_path,'rb') as f:
        img = pickle.load(f)

    stringTmpSplitted = file_path.split("/")[-1:][0].split('_')[0]
    # .split('_')[0]
    print("stringTmpSplitted:",stringTmpSplitted)

    intFaceNumber = numberOfStrFace(stringTmpSplitted)
    print("intFaceNumber:",intFaceNumber)

    # converting 2d to 3d point:
    for x in range(0,2048):
        for y in range(0,2048):
            pointIn3D = conv2dTo3D([x,y], intFaceNumber)
            l2norm = np.linalg.norm(pointIn3D,2)
            pointIn3D = pointIn3D / l2norm
            if(img[x,y]==0):

                dist, ind = tree.query([pointIn3D], k=1)

                # print(ind)  # indices of 1 closest neighbors
                print(dist)  # distances to 1 closest neighbors

                #TODO: labelled obstacles in 3d
                # VAS.append()
                pass
            else:
                pass
                #TODO: VAS.append([pointIn3D,label?])
