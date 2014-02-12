using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class TextLibrary
{	
	private void loadTextHouseItem()
	{
		TextHouseItem = new Dictionary<Tuple<HouseItemType, Completion>, List<string>>();

		//Completion.None
		TextHouseItem.Add(Tuple.Create(HouseItemType.LockedDoor, Completion.None), new List<string>{
			"This door is locked.",
			"The door won't budge.",
			"Even my well placed kick didn't help me get this door to open.",});

		//Completion.Start
		TextHouseItem.Add(Tuple.Create(HouseItemType.FrontCar, Completion.Start), new List<string>{
			"I'm going to drive out of here right after I place this urn over the fireplace."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerMirror, Completion.Start), new List<string>{
			"That broken mirror is still here? I wonder why dad wanted to keep bad memories around."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerChair, Completion.Start), new List<string>{
			"I always hid behind this chair while playing with Liz."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomUrn, Completion.Start), new List<string>{
			"Here you go Dad. You have the place all to yourself, just like you wanted."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.Start), new List<string>{
			"I'm almost positive that's my sisters, Liz, old painting. My dad would hate for me to leave it crooked. If only I had something long to help me get it back on it's second hook."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.Start), new List<string>{
			"I need to place this Urn down where my father wished, on TOP of the fireplace mantle."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomCodex, Completion.Start), new List<string>{
			"What an ornate looking box."});

		//Completion.PlacedUrn
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.PlacedUrn), new List<string>{
			"This firepoker will make it easy to get my sisters painting back into place. My old crazy dad would be proud. Plus, this place is even creepier than I remember. I’ll keep this with me...just in case."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FrontCar, Completion.PlacedUrn), new List<string>{
			"I did what I came for. Time to put this place, and the past, in the mirror..."});

		//Completion.HaveFirePoker
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.HaveFirePoker), new List<string>{
			"There, much better. What was that sound? That nail must have been hooked on something weird behind the wall."});

		//Completion.FixedPainting
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.FixedPainting), new List<string>{ //Firepoker is now gone
			"I'm no Cinderella, I am not cleaning that fireplace out."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.Cryptex1Livingroom, Completion.FixedPainting), new List<string>{
			"This is an odd looking disc. It looks like it would fit in the puzzle box over the fireplace."});

		//Completion.CryptexPieces1
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.CryptexPieces1), new List<string>{
			"I wonder when my sister even found the time to make this painting."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomCodex, Completion.CryptexPieces1), new List<string>{
			"That gold disc I picked up earlier fits right in the front lock of this box. I could probably fit 5 discs in there."});
		
		//Completion.HallwayFloorBoard
		TextHouseItem.Add(Tuple.Create(HouseItemType.Cryptex2Hallway, Completion.HallwayFloorBoard), new List<string>{
			"Did this just fall from the ceiling while I was in the Bedroom?! Seems like the only fixing dad did was in making this place haunted."});

		//Completion.CryptexPieces2
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedMirrorShard, Completion.CryptexPieces2), new List<string>{
			"This shard of glass looks sharp. I should clean up this room before I chance getting bloody."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedPillow1, Completion.CryptexPieces2), new List<string>{
			"My dad was never one to leave things tidy."});

		//Completion.PillowsPlaced
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedPillow1, Completion.PillowsPlaced), new List<string>{
			"Well looky here, another golden disc was under that pillow."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.Cryptex3Masterbed, Completion.PillowsPlaced), new List<string>{
			"That's 3 of 'em. I'm getting the feeling Dad wanted me to find these."});

		//Completion.CryptexPieces3
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedMirrorShard, Completion.CryptexPieces3), new List<string>{
			"I can see my reflection in this shard of glass."});

		//Completion.HaveMirrorShard
		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerMirror, Completion.HaveMirrorShard), new List<string>{
			"This little shard sure covers up the cracks."});

		//Completion.MirrorShardUsed
		TextHouseItem.Add(Tuple.Create(HouseItemType.Cryptex4MirrorShard, Completion.MirrorShardUsed), new List<string>{
			"Who would of thought, a mirror that was rigged to drop one of those gold discs as soon as it was fixed. I can only imagine what magic just picking up this disc has caused."});
		
		//Completion.CryptexPieces4
		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticRockingHorse, Completion.CryptexPieces4), new List<string>{
			"Guess it just needed a touch of reality to stop moving."});

		//Completion.CryptexPieces4
		TextHouseItem.Add(Tuple.Create(HouseItemType.Cryptex5Attic, Completion.BunnyFell), new List<string>{
			"I have quite a few of these discs now. Might be time to figure out what to do with them."});

//		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticBoxes, Completion.None), new List<string>{
//			"These are filled with Liz’s old stuff…"});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticRockingHorse, Completion.None), new List<string>{
//			"The attic used to be empty. This must be where Dad put all of the old things after Mom died."});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingFireplace, Completion.None), new List<string>{
//			"I don’t remember this being here… it looks like there’s a note tucked behind the canvas."});
	}
	
	private void loadTextRoom()
	{
		TextRoom = new Dictionary<Tuple<Room, Completion>, List<string>>();
		TextRoom.Add(Tuple.Create(Room.FrontHouse, Completion.Start), new List<string>{
@"It's been so long since I've been home. I hardly remember this place, but Dad wanted his ashes placed over the mantle. I'll just put them there and leave.
Strange to have an unfinished inscription on the urn though. All it says is, ""YOU ARE""."});
		
		TextRoom.Add(Tuple.Create(Room.Bedroom, Completion.CryptexPieces1), new List<string>{
			@"This place sure is creaky. I just hope the floor or ceiling doesn't fall out."});
		TextRoom.Add(Tuple.Create(Room.Bedroom, Completion.CryptexPieces2), new List<string>{
			@"More things are sure to fall from the ceiling just by stepping in my old bedroom."});
		TextRoom.Add(Tuple.Create(Room.Hallway, Completion.CryptexPieces2), new List<string>{
			@"...or new doors unlock."});
		TextRoom.Add(Tuple.Create(Room.Hallway, Completion.CryptexPieces4), new List<string>{
			@"Those stairs must go up to the attic."});
		
		TextRoom.Add(Tuple.Create(Room.Attic, Completion.CryptexPieces4), new List<string>{
			@"Umm...That would be a rocking horse all right. Have I mentioned how this place is increasingly creepy? I hope the last disc is up here."});
	}
}