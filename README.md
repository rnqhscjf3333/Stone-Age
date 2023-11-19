# Stone-Age(석기시대)
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/3e167dac-84be-4b9f-9f99-5de730c4370e)<br>
- 석기시대 메타버스 게임

## 🖥️ 프로젝트 소개
- 성인식을 준비하는 구석기인을 체험하며 구석기에 대해서 학습하는 메타버스 게임
- 프로젝트 부트캠프 유니티 성과발표회 본선진출작
- 웅진씽크빅에서 제공한 기획서를 토대로 제작한 기업연계 프로젝트

## 🕰️ 제작기간
- 23.10.20~23.11.17 (1개월)

## 🧑‍🤝‍🧑 멤버구성
- 팀장 : 김예지 - 대화문 / 인벤토리 / 설정 / 퀘스트
- **팀원 : 구본관 - 플레이어 / 동물 / 식물 / 채집 / 사냥 / 오프닝 / 엔딩**
- 팀원 : 김시온 - 석기제작 미니게임
- 팀원 : 박세인 - 맵 / 미니맵 / 오디오 / 학습자료

## ⚙️ 개발엔진
- 유니티

## 📌 구현 내용
### 오프닝
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/6f1866ec-e69e-4a87-9bd8-c3588d4050d9)
### [오프닝 영상](https://youtu.be/8gyJf4QwdNQ)
- 맵구성, 동물, 시대 등의 정보를 포함
- 동굴에 도착해서 주인공을 보여주며 종료

### 플레이어
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/3e167dac-84be-4b9f-9f99-5de730c4370e)<br>
### [플레이어 조작 및 채집 영상](https://youtu.be/S2GwhcFLdsc)
- 이동 / 점프 / 대쉬 / 공격 / 조준 / 수영
- 유니티 Starter Assets을 기반으로 제작

### 채집(수집)
1. Physics.OverlapSphere로 주변 아이템 탐지
2. 아이템으르 탐지하면 상단의 수집버튼 활성화
3. 수집버튼을 터치하면 탐지한 아이템들을 수집UI의 Queue에 삽입
4. 수집UI에서 Coroutine으로 일정시간마다 Deque해서 출력

### 석기
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/2de45f6b-eac9-4293-9661-c23b95d680f7) <br>
### [토끼사냥 영상](https://youtu.be/q209h6_pOpw)
- 착용석기에 따라 다른 공격모션과 공격범위

### 활 조준
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/5c657a91-aa68-4a4b-a52a-33d92b9b976c)
### [사슴사냥 영상](https://youtu.be/3Xe7xw7O4Xc)
- 가상카메라 2개 사용 -> 선형보간으로 카메라 위치 변경
- 조준선 표시, 이동속도 감소, 카메라 감도 감소
- ScreenPointToRay 사용 -> 화면 정중앙에서 Ray를 발사해서 조준점 생성
- 조준점 방향으로 화살 발사
  
### 동물
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/dc243ffc-76d8-4a03-8a69-d314a1460fdc)
- 5종류의 동물, NavMesh 사용
- Physics.OverlapSphere로 플레이어 탐지
- 플레이어 방향과 전방방향을 이용해서 각도 계산->플레이어가 전방에 있으면 탐지판정
- 탐지 성공시 초식동물이면 도망, 육식동물이면 추적 후 공격

### 보스(곰)
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/cc1ea1f2-e7d3-439f-b097-697cb4ec96c9)
### [곰 보스전 영상](https://youtu.be/mqou62arm2g)
- 성인식을 완료하기 위한 최종 관문
1. 플레이어에게 돌진 후 2번 공격
2. 땅에서 돌을 집어올린 후 플레이어 방향으로 투척
3. 피가 절반 이하가 되면 이동속도 증가

  ### 엔딩
![image](https://github.com/rnqhscjf3333/Stone-Age/assets/41632370/5065200e-6ad1-4374-b31e-081b63d5e81c)
### [엔딩 영상](https://youtu.be/cODVeFC8LJ4)
- 곰을 잡은것을 축하 -> 성인이 된것을 축하하는 잔치
- 음악에 맞춰서 카메라와 애니메이션이 출력되도록 제작
