import pickle

import numpy as np

from matplotlib import pyplot as plt

from sklearn.neighbors import KDTree


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "dVASpertamFiltered.pickle"

with open(file_path,'rb') as f:
    dVASfiltered = pickle.load(f)


xs = []
ys = []
zs = []

for key in dVASfiltered.keys():
    pass
    # print(key, '->', dVASfiltered[key])
    # print(key[0])
    # print(key[1])
    # print(key[2])
    xs.append(key[0])
    ys.append(key[1])
    zs.append(key[2])

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
img = ax.scatter(xs, ys, zs,s=0.2)
fig.colorbar(img)

plt.show()
pass