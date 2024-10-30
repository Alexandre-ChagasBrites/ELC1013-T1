# ELC1013-T1

Não acho que quem ganhar ou quem perder, nem quem ganhar nem perder, vai ganhar ou perder. Vai todo mundo perder.

## Exemplos

```
ifthen{Se p{o time joga bem}, q{ganha o campeonato}}.
ifthen{Se o time not{não p{joga bem}}, r{o técnico é culpado}}.
ifthen{Se q{o time ganha o campeonato}, s{os torcedores ficam contentes}}.
Os torcedores not{não s{estão contentes}}.
Logo, r{o técnico é culpado}.
```

```
thenif{p{O participante vai ao paredão} se or{q{o lider o indica} ou r{os colegas o escolhem}}}.
ifthen{Se and{p{o participante vai ao paredão} e s{chora}}, então t{ele conquista o público}}.
ifthen{Se t{o participante conquista o público}, ele not{não u{é eliminado}}}.
and{q{O lider indicou um participante} e u{ele foi eliminado}}.
Logo, o participante not{não s{chorou}}.
```

```
ifthen{Se or{p{o programa é bom} ou q{passa no horário nobre}}, r{o público o assiste}}.
ifthen{Se and{r{o público assiste} e s{gosta}}, então t{a audiência é alta}}.
ifthen{Se t{a audiência é alta}, u{a propaganda é cara}}.
O programa, and{q{passa no horário nobre}, mas not{u{a propaganda é barata}}}.
Logo, o público not{não s{gosta do programa}}.
```

```
ifthen{Se p{os meus óculos estão na mesa da cozinha} então q{eu os vi no café da manhã}};
or{r{Eu estava lendo o jornal na sala de estar} ou s{eu estava lendo o jornal na cozinha}};
ifthen{Se r{eu estava lendo o jornal na sala de estar} então t{meus óculos estão na mesa do café}};
not{Eu não q{vi meus óculos no café da manhã}};
ifthen{Se u{eu estava lendo um livro na cama} então v{meus óculos estão na mesa de cabeceira}};
ifthen{Se s{eu estava lendo o jornal na cozinha} então p{meus óculos estão na mesa da cozinha}};
not{t{}}
```
