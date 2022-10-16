import pickle

import numpy as np

from matplotlib import pyplot as plt

from sklearn.neighbors import KDTree


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "dVASpertamFilteredWithLabels.pickle"

with open(file_path,'rb') as f:
    dVASfiltered = pickle.load(f)


xs = []
ys = []
zs = []

mucolors = []

for key in dVASfiltered.keys():
    pass
    # print(key, '->', dVASfiltered[key])
    # print(key[0])
    # print(key[1])
    # print(key[2])
    xs.append(key[0])
    ys.append(key[1])
    zs.append(key[2])
    mucolors.append(key[2]%3000)
    # mucolors.append((key[0]+key[1]+key[2])%100)

# dist, ind = tree.query([[500,500,PR]], k=5)
# print(ind)  # indices of 5 closest neighbors
# print(dist)  # distances to 5 closest neighbors

# for t3dPoint in range(len(X)):
#     xs.append(X[t3dPoint][0])
#     ys.append(X[t3dPoint][1])
#     zs.append(X[t3dPoint][2])

import matplotlib.pyplot as plt

fig = plt.figure(figsize=(12,7))
ax = fig.add_subplot(projection='3d')
# img = ax.scatter(xs, ys, zs,s=1)
# img = ax.scatter(xs, ys, zs,s=0.2)
img = ax.scatter(xs, ys, zs, c=mucolors,s=0.2)
# img = ax.scatter(xs, ys, s=0.2)
fig.colorbar(img)

plt.show()
pass

counting2 = 0
counting3 = 0
counting4 = 0
for key in dVASfiltered.keys():
    pass
    # print(key, '->', dVASfiltered[key])
    counted = dVASfiltered[key]
    if(counted==2):
        counting2 = counting2 + 1
    if(counted==3):
        counting3 = counting3 + 1
    if(counted==4):
        counting4 = counting4 + 1

print("counting2",counting2)
print("counting3",counting3)
print("counting4",counting4)