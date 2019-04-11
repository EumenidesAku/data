# 使用说明

单机发号器使用的时候需要在配置文件配置WorkNode信息，
WorkNode信息的申请联系有关负责人。

采用如下的结构

字段名 | 字段长度 |值 | 备注
---|---| ---|---
Sign|4 |2| 协议号
Timestamp | 32 |  | 时间戳
WorkNode | 16 | 配置文件 |工作号
Sequence | 12|  |随机数

为了避免WorkNode乱填写导致的Id紊乱，实际使用Id生成器需要搭配以下2个参数

+ WorkNode
+ WorkNodeSec


## 备注

Worknode=0的情况都不使用，预留给错误




