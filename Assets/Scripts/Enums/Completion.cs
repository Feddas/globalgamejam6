using UnityEngine;
using System.Collections;

public enum Completion
{
	None,
	Start,
	PlacedUrn,
	HaveFirePoker,
	FixedPainting,
	CryptexPieces1, //unlock upstairs hallway and bedroom
	
	HallwayFloorBoard,
	CryptexPieces2, //unlock masterbed

	PillowsPlaced,
	CryptexPieces3,
	
	HaveMirrorShard,
	MirrorShardUsed,
	CryptexPieces4, //unlock attic

	StoppedRockingHorse,
	BunnyFell,
	CryptexPieces5, //unlock cryptex game

	CryptexPiecesPutIn,
	CodexPhotoViewed,
}
//TODO: rename CryptexPieces1 to HouseItemType names; ie Cryptes1LivingRoom