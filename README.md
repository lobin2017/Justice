# Justice: The Last Epithet

## 조작 방법

* WASD 키를 이용하여 플레이어를 이동합니다.
* 마우스 위치를 기준으로 공격 방향을 조준합니다.
* 마우스 좌클릭으로 공격합니다.
* E 키를 이용하여 포탈 및 오브젝트와 상호작용합니다.
* Unity Input System을 사용하여 입력을 처리하였습니다.

---

## 프로젝트 개요

Justice: The Last Epithet는 Unity 6를 기반으로 제작한 2.5D 탑뷰 액션 RPG 프로토타입입니다.

플레이어는 로비에서 던전에 입장하여 보스와 전투를 진행하며 다음 스테이지로 이동합니다.

본 프로젝트는 플레이어와 보스 시스템을 컴포넌트 단위로 분리하여 유지보수가 가능하도록 설계하였으며, ScriptableObject를 이용한 데이터 관리 구조를 적용하였습니다.

---

## 구현 기능

### 1-1. 플레이어 이동

* Input System 기반 이동 구현
* Rigidbody2D를 이용한 이동 처리
* 8방향 이동 구현
* 이동 상태에 따라 Idle / Walk 애니메이션 전환

---

### 1-2. 플레이어 공격

* 마우스 위치를 기준으로 공격 방향 계산
* 공격 쿨타임 적용
* 공격 거리 제한
* 공격 각도 판정
* 거리와 각도를 모두 만족하는 대상에게 피해 적용

---

### 1-3. 플레이어 체력

* IDamageable 인터페이스 구현
* 체력 감소 처리
* 사망 처리 구현

---

### 2-1. 보스 AI

* 플레이어 탐지
* 플레이어 추적
* 공격 범위 진입 시 공격 상태 전환
* FSM(State) 기반 상태 관리

---

### 2-2. 보스 전투

* 공격
* 피격
* 체력 감소
* 사망 처리
* 2페이즈 진입

---

### 2-3. 보스 데이터

* ScriptableObject를 이용한 보스 데이터 관리
* 보스별 능력치 관리
* 이동속도
* 체력
* 공격력
* 공격 쿨타임 관리

---

### 3. Scene 구성

#### Lobby Scene

* 플레이어 생성
* NPC 배치
* 포탈 구현
* 카메라 추적

#### Boss Room Scene

* 보스 생성
* 플레이어와 보스 전투
* 보스 처치 후 진행

---

### 4. 카메라

* Main Camera를 플레이어 중심으로 추적
* Lerp를 이용한 부드러운 이동 구현

---

### 5. Physics 2D

* Rigidbody2D 사용
* Collider2D 사용
* Trigger 사용
* Physics2D.OverlapCircle을 이용한 플레이어 탐지 구현

---

### 6. Animation

* Animator 사용
* 플레이어 8방향 애니메이션
* 보스 애니메이션
* 상태에 따른 애니메이션 전환

---

### 7. UI

* Canvas 구성
* EventSystem 구성
* 보상 UI 구현

---

### 8. 프로젝트 구조

플레이어와 보스의 기능을 역할별로 분리하여 관리하였습니다.

#### Player

* PlayerMovement
* PlayerAttack
* PlayerHealth
* PlayerAnimationController
* PlayerStatus

#### Boss

* BossController
* BossMovement
* BossAttack
* BossHealth
* BossAnimation
* BossSight
* BossData

---

### 9. 리소스 관리

프로젝트 리소스는 기능별 폴더로 분리하여 관리하였습니다.


---

## 사용한 Unity 기능

* Input System
* Animator
* Rigidbody2D
* Collider2D
* Physics2D
* Trigger
* ScriptableObject
* SceneManager
* UI(Canvas)

---

## 프로젝트 설명

본 프로젝트는 Unity의 컴포넌트 기반 구조를 활용하여 플레이어와 보스 시스템을 각각 독립적으로 설계하였습니다.

플레이어와 보스의 이동, 공격, 체력, 애니메이션을 각각 분리하여 유지보수가 가능하도록 구현하였으며, ScriptableObject를 이용하여 보스 데이터를 관리하도록 설계하였습니다.

이를 통해 기능 추가 및 수정이 용이한 구조를 목표로 제작하였습니다.

---

## 향후 개선 사항
* 보스 패턴 다양화
* 플레이어 스킬 시스템
* 대시 시스템
* 가문 시스템
* NPC 대화 시스템
* 사운드 및 연출 개선
* UI 개선

---