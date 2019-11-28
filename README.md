# RogueLiteTestDemo

### Start At 2019.11.28

前期 demo 设计功能：

1. 游戏基本玩法： Plane 生成对象， NEVMesh 寻路冲击，敌我双方生成规则
2. AI 设计： 移动控制状态机
3. 游戏场景设计：场景优化， 爆炸效果优化，音效优化
4. 游戏流程调整：游戏流程添加，Settle 结算界面添加
5. 游戏数值调整：游戏数值调整，生成模式调整。

6. 生成器-> delegate？no -> childGenerateModel -> with own ctrl
7. Sample Pool
8. MovingObject


需求 Plugins：

1. HighLight
2. DOTween
3. Boom
4. Fx Effect
5. Camera Effect
6. JsonNetWork

添加调整方案：
1. 随着等级的提升，建造士兵需要的消耗的能量值增加
2. Inspector 调整，为了方便增删属性，应该用 bool 将可以添加的属性标注出来，然后再添加到 PropTypeList 中