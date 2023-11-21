# 4조 스파르타 던전 (Text Game) 

</br>
던전에 도착한 캐릭터가 전투를 하는 게임. 기본 턴제로 진행되며 몬스터들과 상호작용을 통해 캐릭터의 레벨업을 시킬 수 있다. 

</br>

## 주요 기능
* **게임 시작 화면**
  
![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/b306cee6-9d7f-45c3-87b6-0ca7403c6cdc)

게임의 기본 타이틀 화면. 아무 키나 입력시 게임을 진행할 수 있다.

  * **이름 설정**

    ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/f14c8360-c368-45c5-aa3d-df2da86b7098)
    
    게임 진행시에 제일 먼저 유저는 캐릭터의 이름을 설정한다. 빈 이름으로 설정해 둘 수 없으며 설정을 완료할때까지 **플레이어의 이름은 공백일 수 없습니다* 라는 경고문을 띄운다.
  * **직업 설정**

    ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/3a248e30-4cbb-4bc4-ae4f-0e21a3750e84)

    전사, 마법사, 궁수 세가지의 직업이 있다. 각 직업마다 스탯과 쓸 수 있는 스킬들이 다르다.


* **상태보기**

  ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/d08f7853-c3e9-4a76-8e6e-541d0e8c575c)

  캐릭터의 기본 상태를 표시해주는 창. 유저가 선택한 직업에 따라 설정된 공격력, 방어력, 체력, 마나를 보여준다. 또한 장착한 장비에 따라 추가되는 스탯을 **(+ (추가된 스탯))** 형식으로 보여준다.
  또 마나와 체력은 (현재 캐릭터가 가지고 있는 마나 or 체력 수치) / (전체 최대 마나 or 체력 수치) 식으로 보여줘서 상한치를 한눈에 확인할 수 있게 해준다. 

  
* **인벤토리**

  ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/f6dbed28-97ea-4805-a299-830ba200d83f)

  현재 캐릭터가 가지고 있는 아이템들의 정보를 보여준다. 종류는 무기구, 방어구, 포션 3가지가 있다.
  가지고 있는 아이템의 이름, 종류, 등급, 가격 정보를 보여준다. 

  * **장착 관리**

    ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/47dbfb22-1b6b-43a1-add0-5cabb21c73fe)

    현재 하이라이트 되어있는 아이템을 장착 또는 장착 해제를 할 수 있다. 똑같은 종류의 무기를 장착하려고 하면 (낡은검 1을 장착한 상태로 낡은검2 장착시도) 이 전에 장착하고 있던 무기는 해제된다.
    장착된 장비는 앞에 **[E]** 라고 표시해준다. 장착된 아이템은 장착관리 페이지를 벗어나도 어떤 장비가 장착 중인지 표시를 해준다. 

    장비 장착 후 상태보기 창

    ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/14e5ec88-9775-4886-a640-f51e2fc4d62d)
    
  *  **아이템 사용**
    
    ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/daf5e930-c63d-42bf-9941-cfab6a2c973b)

    아이템 사용하기 창을 들어가면 포션류 아이템들을 보여준다. 일반 회복 물약과 마나 회복 물약 두가지가 있으며, 가득 찬 상태에서는 "가득 찬 상태입니다" 메세지 창과 함께 아이템이 사용되지 않는다. 
  
  *  **아이템 버리기**

  ![image](https://github.com/suhyunChun/-4-Sparta-Text-Game-/assets/89771577/9989e6c2-55fb-4299-a202-f70a4cf8935a)


  *  **아이템 정렬**
 
    
* **상점**

* **던전입장(전투)**
  * **레벨업**
  * **크리티컬 & 회피**
  * **스킬 사용**
  * **물약 사용**
    

## 추가 기능 

## 기능 세부 설명




    


##  기술 스택

![C#](https://img.shields.io/badge/-C%23-%7ED321?logo=Csharp&style=flat)

