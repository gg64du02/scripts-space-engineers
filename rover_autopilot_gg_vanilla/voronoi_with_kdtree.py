import pickle

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

                # TODO:faire la transformation en sphere ?
                pointIn3D = conv2dTo3D([x,y], intFaceNumber)
                # print("pointIn3D",pointIn3D)
                X.append(pointIn3D)

                pass


    # appending to the planets points
    # X is going to the planets points
    
    tree = KDTree(X, leaf_size=2)

    # dist, ind = tree.query(X[:1], k=3)
    dist, ind = tree.query([[500,500,PR]], k=5)
    print(ind)  # indices of 5 closest neighbors

    print(dist)  # distances to  closest neighbors

    pass

