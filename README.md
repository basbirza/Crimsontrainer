# Crimson Desert Trainer

A WinForms game trainer for **Crimson Desert** (single-player, offline).  
Memory offsets sourced from the community CE table at [opencheattables.com](https://opencheattables.com/viewtopic.php?t=1836) (author: bbfox0703, updated 2026-06-05).

## Features

| Hotkey | Cheat | Status |
|--------|-------|--------|
| F1 | Infinite Health | ✅ Real offsets |
| F2 | Infinite Stamina | ✅ Real offsets |
| F3 | Infinite Spirit | ✅ Real offsets |
| F4 | God Mode (HP fill) | ✅ Working |
| — | Infinite Gold/Silver | ⚠️ TODO (item system, different AOB) |
| — | No Cooldown | ⚠️ TODO (needs cooldown offset research) |

## How it works

Crimson Desert stores player stats in a dynamic entity struct. The trainer scans
all committed memory regions at runtime to find the struct, then writes values
every 100 ms.

### Player entity offsets (from CE table, version 2026-06-05)

| Offset | Field | Type |
|--------|-------|------|
| +0x008 | Current HP | Int64 |
| +0x018 | Max HP | Int64 |
| +0x5A8 | Current Stamina | Int64 |
| +0x5B8 | Max Stamina | Int64 |
| +0x638 | Current Spirit | Int64 |
| +0x648 | Max Spirit | Int64 |

Values are scaled internally (display * ~100 for current, display * ~10 for max).
The trainer writes `max * 10` to current each tick to keep bars full.

## Usage

1. Build and run the trainer **as Administrator** (required for process memory access).
2. Launch **Crimson Desert** and load a save.
3. Wait for the trainer to show **● ATTACHED**.
4. Once your character is fully loaded in-game (HP bar visible), click **🔍 Find Entity**.
5. The entity address will appear. Now toggle cheats with buttons or hotkeys.

> If "Find Entity" returns nothing: enter combat, take a hit, then try again — this
> ensures the entity struct is populated in memory.

## Build

Requires **.NET 6 SDK** on **Windows x64**.

```bash
cd CrimsonTrainer
dotnet build -c Release
# Binary in: bin/Release/net6.0-windows/CrimsonTrainer.exe
```

Run the `.exe` as Administrator.

## If offsets break after a game update

1. Open Cheat Engine 7.6+, attach to `CrimsonDesert.exe`.
2. Enable "Get HP address: Step 1 & 2 - AOB mode" from the community CT.
3. Open the item menu in-game; HP/Sta/Spi addresses will populate.
4. Update `OffsetCurrentHp`, `OffsetCurrentSta`, `OffsetCurrentSpi` in the relevant cheat files.

## Silver / Gold (not yet implemented)

Silver in Crimson Desert is item ID 980 in the inventory table, handled by a separate
AOB injection (`INJECT_INF_ARROW_N_OTHERS`) that intercepts item decrease code.
This is more complex than stat cheats and is marked TODO in `InfiniteGold.cs`.
