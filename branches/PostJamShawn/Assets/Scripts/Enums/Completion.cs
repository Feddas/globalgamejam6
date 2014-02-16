using UnityEngine;
using System.Collections;

public enum Completion
{
	None,
	Start,
	PlacedUrn,
	HaveFirePoker,
	FixedPainting,
	Cryptex1Livingroom, //unlock upstairs hallway and bedroom
	
	HallwayFloorBoard,
	Cryptex2Hallway, //unlock masterbed

	PillowsPlaced,
	Cryptex3Masterbed,
	
	HaveMirrorShard,
	MirrorShardUsed,
	Cryptex4MirrorShard, //unlock attic

	StoppedRockingHorse,
	BunnyFell,
	Cryptex5Attic, //unlock cryptex game

	CryptexPiecesPutIn,
	CodexPhotoViewed,
}