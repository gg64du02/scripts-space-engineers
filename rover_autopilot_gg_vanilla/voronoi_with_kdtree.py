import pickle

from matplotlib import pyplot as plt

folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"


# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
files = {"back_sobelBitmap_64_min.pickle"}

# sobelBitmap_64_min_TargetFilePath = stringTmpSplitted + "_sobelBitmap_64_min.pickle"


full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)

for file_path in full_files_path:

    with open(file_path,'rb') as f:
        img = pickle.load(f)

    # stringTmpSplitted = file_path.split(".")[0]

    plt.imshow(img)
    plt.show()