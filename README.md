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
3. 佛了。。。写错了 solider 是动态生成的对象，因此不应该直接去修改它的附加属性值，也就是说，不应该 Custom PlayerElf


### Custom Inspector
1. 关于如何设置自定义编辑器呢？
我们期望能够通过遍历解决代码太多的麻烦。
2. demo 内暂时放弃 ECS 的设计模式，因为没有有余的时间去处理这部分代码。