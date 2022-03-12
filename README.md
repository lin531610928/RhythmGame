# RhythmGame
新项目制作中


{ Game   : ed8_ps5_D3D11.exe
  Version: 
  Date   : 2022-03-13
  Author : user

  This script does blah blah blah
}

[ENABLE]
//code from here to '[DISABLE]' will be used to enable the cheat

 
 
aobscanmodule(INJECT,ed8_ps5_D3D11.exe,41 0F 28 C2 F3 0F 10 0D CF A5 81 00) // should be unique
alloc(newmem,$1000,INJECT)

label(code)
label(return)

newmem:

code:
  movaps xmm0,xmm10
  movss xmm1,[ed8_ps5_D3D11.exe+AC9F38]
  jmp return

INJECT:
  jmp newmem
  nop 7
return:
registersymbol(INJECT)

[DISABLE]
//code from here till the end of the code will be used to disable the cheat
INJECT:
  db 41 0F 28 C2 F3 0F 10 0D CF A5 81 00

unregistersymbol(INJECT)
dealloc(newmem)

{
// ORIGINAL CODE - INJECTION POINT: ed8_ps5_D3D11.exe+2AF95D

ed8_ps5_D3D11.exe+2AF931: E8 7A 28 0D 00           - call ed8_ps5_D3D11.exe+3821B0
ed8_ps5_D3D11.exe+2AF936: 80 B8 82 B8 06 00 00     - cmp byte ptr [rax+0006B882],00
ed8_ps5_D3D11.exe+2AF93D: 74 18                    - je ed8_ps5_D3D11.exe+2AF957
ed8_ps5_D3D11.exe+2AF93F: 41 0F 28 C6              - movaps xmm0,xmm14
ed8_ps5_D3D11.exe+2AF943: F3 0F 5C 05 B5 1E 93 00  - subss xmm0,[ed8_ps5_D3D11.exe+BE1800]
ed8_ps5_D3D11.exe+2AF94B: 41 0F 2F C2              - comiss xmm0,xmm10
ed8_ps5_D3D11.exe+2AF94F: 76 2A                    - jna ed8_ps5_D3D11.exe+2AF97B
ed8_ps5_D3D11.exe+2AF951: 44 0F 28 D0              - movaps xmm10,xmm0
ed8_ps5_D3D11.exe+2AF955: EB 24                    - jmp ed8_ps5_D3D11.exe+2AF97B
ed8_ps5_D3D11.exe+2AF957: F3 0F 10 54 24 64        - movss xmm2,[rsp+64]
// ---------- INJECTING HERE ----------
ed8_ps5_D3D11.exe+2AF95D: 41 0F 28 C2              - movaps xmm0,xmm10
// ---------- DONE INJECTING  ----------
ed8_ps5_D3D11.exe+2AF961: F3 0F 10 0D CF A5 81 00  - movss xmm1,[ed8_ps5_D3D11.exe+AC9F38]
ed8_ps5_D3D11.exe+2AF969: F3 0F 5C C2              - subss xmm0,xmm2
ed8_ps5_D3D11.exe+2AF96D: 0F 2F C8                 - comiss xmm1,xmm0
ed8_ps5_D3D11.exe+2AF970: 76 09                    - jna ed8_ps5_D3D11.exe+2AF97B
ed8_ps5_D3D11.exe+2AF972: 44 0F 28 D2              - movaps xmm10,xmm2
ed8_ps5_D3D11.exe+2AF976: F3 44 0F 58 D1           - addss xmm10,xmm1
ed8_ps5_D3D11.exe+2AF97B: F3 0F 10 87 08 0A 00 00  - movss xmm0,[rdi+00000A08]
ed8_ps5_D3D11.exe+2AF983: F3 0F 11 45 E8           - movss [rbp-18],xmm0
ed8_ps5_D3D11.exe+2AF988: 0F 10 45 A0              - movups xmm0,[rbp-60]
ed8_ps5_D3D11.exe+2AF98C: 48 8D 55 A0              - lea rdx,[rbp-60]
}
