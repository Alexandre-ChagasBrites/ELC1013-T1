# ELC1013-T1

Não acho que quem ganhar ou quem perder, nem quem ganhar nem perder, vai ganhar ou perder. Vai todo mundo perder.

## Exemplos

if{Se p{o time joga bem}}, then{q{ganha o campeonato}}.
if{Se o time not{não p{joga bem}}}, then{r{o técnico é culpado}}.
if{Se q{o time ganha o campeonato}}, then{s{os torcedores ficam contentes}}.
Os torcedores not{não s{estão contentes}}.
Logo, r{o técnico é culpado}.

then{p{O participante vai ao paredão}} if{se q{o lider o indica} or{ou} r{os colegas o escolhem}}.
if{Se p{o participante vai ao paredão} and{e} s{chora}}, then{então t{ele conquista o público}}.
if{Se t{o participante conquista o público}}, ele not{não u{é eliminado}}.
q{O lider indicou um participante} and{e} u{ele foi eliminado}.
Logo, o participante not{não s{chorou}}.

if{Se p{o programa é bom} or{ou} q{passa no horário nobre}}, then{r{o público o assiste}}.
if{Se r{o público assiste} and{e} s{gosta}}, then{então t{a audiência é alta}}.
if{Se t{a audiência é alta}}, u{a propaganda é cara}.
O programa, q{passa no horário nobre}, and{mas} not{u{a propaganda é barata}}.
Logo, o público not{não s{gosta do programa}}.

<pre><if>Se <var name="p">o programa é bom</var> <or>ou</or> <var name="q">passa no horário nobre</var></if>, <then><var name="r">o público o assiste</var></then>.</pre>
<pre><if>Se <var name="r">o público assiste</var> <and>e</and> <var name="s">gosta</var></if>, <then>então <var name="t">a audiência é alta</var></then>.</pre>
<pre><if>Se <var name="t">a audiência é alta</var></if>, <then><var name="u">a propaganda é cara</var></then>.</pre>
<pre>O programa, <var name="q">passa no horário nobre</var>, <and>mas</and> <not><var name="u">a propaganda é barata</var></not>.</pre>
<con>Logo, o público <not>não <var name="s">gosta do programa</var></not>.</con>
