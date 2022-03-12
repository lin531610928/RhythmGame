# RhythmGame
新项目制作中


{ Game   : ed8_ps5_D3D11.exe
  Version: 
  Date   : 2022-03-13
  Author : user

  This script does blah blah blah
}

define(address,"ed8_ps5_D3D11.exe"+2AF970)
define(bytes,76 09 44 0F 28 D2)

[ENABLE]
//code from here to '[DISABLE]' will be used to enable the cheat


 
 
assert(address,bytes)
alloc(newmem,$1000,"ed8_ps5_D3D11.exe"+2AF970)

label(code)
label(return)

newmem:

code:
  jna ed8_ps5_D3D11.exe+2AF97B
  movaps xmm10,xmm2
  jmp return

address:
  jmp newmem
  nop
return:

[DISABLE]
//code from here till the end of the code will be used to disable the cheat
address:
  db bytes
  // jna ed8_ps5_D3D11.exe+2AF97B
  // movaps xmm10,xmm2

dealloc(newmem)

{
// ORIGINAL CODE - INJECTION POINT: ed8_ps5_D3D11.exe+2AF970

ed8_ps5_D3D11.exe+2AF943: F3 0F 5C 05 B5 1E 93 00     - subss xmm0,[ed8_ps5_D3D11.exe+BE1800]
ed8_ps5_D3D11.exe+2AF94B: 41 0F 2F C2                 - comiss xmm0,xmm10
ed8_ps5_D3D11.exe+2AF94F: 76 2A                       - jna ed8_ps5_D3D11.exe+2AF97B
ed8_ps5_D3D11.exe+2AF951: 44 0F 28 D0                 - movaps xmm10,xmm0
ed8_ps5_D3D11.exe+2AF955: EB 24                       - jmp ed8_ps5_D3D11.exe+2AF97B
ed8_ps5_D3D11.exe+2AF957: F3 0F 10 54 24 64           - movss xmm2,[rsp+64]
ed8_ps5_D3D11.exe+2AF95D: 41 0F 28 C2                 - movaps xmm0,xmm10
ed8_ps5_D3D11.exe+2AF961: F3 0F 10 0D CF A5 81 00     - movss xmm1,[ed8_ps5_D3D11.exe+AC9F38]
ed8_ps5_D3D11.exe+2AF969: F3 0F 5C C2                 - subss xmm0,xmm2
ed8_ps5_D3D11.exe+2AF96D: 0F 2F C8                    - comiss xmm1,xmm0
// ---------- INJECTING HERE ----------
ed8_ps5_D3D11.exe+2AF970: 76 09                       - jna ed8_ps5_D3D11.exe+2AF97B
// ---------- DONE INJECTING  ----------
ed8_ps5_D3D11.exe+2AF972: 44 0F 28 D2                 - movaps xmm10,xmm2
ed8_ps5_D3D11.exe+2AF976: F3 44 0F 58 D1              - addss xmm10,xmm1
ed8_ps5_D3D11.exe+2AF97B: F3 0F 10 87 08 0A 00 00     - movss xmm0,[rdi+00000A08]
ed8_ps5_D3D11.exe+2AF983: F3 0F 11 45 E8              - movss [rbp-18],xmm0
ed8_ps5_D3D11.exe+2AF988: 0F 10 45 A0                 - movups xmm0,[rbp-60]
ed8_ps5_D3D11.exe+2AF98C: 48 8D 55 A0                 - lea rdx,[rbp-60]
ed8_ps5_D3D11.exe+2AF990: F3 44 0F 10 8D 50 01 00 00  - movss xmm9,[rbp+00000150]
ed8_ps5_D3D11.exe+2AF999: 48 8D 4D 00                 - lea rcx,[rbp+00]
ed8_ps5_D3D11.exe+2AF99D: 45 0F 28 C7                 - movaps xmm8,xmm15
ed8_ps5_D3D11.exe+2AF9A1: 41 0F 28 F6                 - movaps xmm6,xmm14
}
