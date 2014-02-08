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
			"This firepoker will make it easy to get my sisters painting back into place. My old crazy dad would be proud. Plus This place is even creepier than I remember. I’ll keep this with me...just in case."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FrontCar, Completion.PlacedUrn), new List<string>{
			"I did what I came for. Time to put this place, and the past, in the mirror..."});

		//Completion.HaveFirePoker
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.HaveFirePoker), new List<string>{
			"There, much better. What was that sound? That nail must have been hooked on something weird behind the wall."});

		//Completion.FixedPainting
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.FixedPainting), new List<string>{ //Firepoker is now gone
			"I'm no Cinderella, I am not cleaning that fireplace out."});

		//Completion.CryptexPieces1
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.CryptexPieces1), new List<string>{
			"I wonder if my sister painted this house."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomCodex, Completion.CryptexPieces1), new List<string>{
			"That gold disc I picked up earlier fits right in the front lock of this box. I could probably fit 5 discs in there."});

		//Completion.CryptexPieces2
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedMirrorShard, Completion.CryptexPieces2), new List<string>{
			"I can see my reflection in this shard of glass. I should clean up this room before I chance getting bloody."});

//		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticBoxes, Completion.None), new List<string>{
//			"These are filled with Liz’s old stuff…"});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticRockingHorse, Completion.None), new List<string>{
//			"The attic used to be empty. This must be where Dad put all of the old things after Mom died."});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.BedroomBed, Completion.None), new List<string>{
//			"My old bed… it’s just as I left it. I think I’ll lay down for a moment…"});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.BedroomPainting, Completion.None), new List<string>{//Poster
//			"I can’t believe I listened to them…"});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomCodex, Completion.None), new List<string>{//without all pieces
//			"This looks like it would fit in the puzzle box. I guess these things are just lying about. I wonder how many more there are."});
//
//		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingFireplace, Completion.None), new List<string>{
//			"I don’t remember this being here… it looks like there’s a note tucked behind the canvas."});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingsCreepy, Completion.None), new List<string>{
//			"The original home owners. Mom said these paintings had charm, but I always felt like they were staring at me."});
//
//		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerMirror, Completion.None), new List<string>{//without mirror shard
//			"The mirror...",
//			"I remember..."});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.DoorAttic, Completion.None), new List<string>{//without FirePoker
//			"None of us were ever able to open the attic once the chord broke. We always used something to reach up there…"});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.HallDoorLockedBathroom, Completion.None), new List<string>{
//			"It's just the bathroom."});
//		TextHouseItem.Add(Tuple.Create(HouseItemType.HallFloorboard, Completion.None), new List<string>{
//			"Wasn’t Dad supposed to fix this? It looks loose…"});
	}
	
	private void loadTextRoom()
	{
		TextRoom = new Dictionary<Tuple<Room, Completion>, List<string>>();
		TextRoom.Add(Tuple.Create(Room.FrontHouse, Completion.Start), new List<string>{
@"It's been so long since I've been home. I hardly remember this place, but Dad wanted his ashes placed over the mantle. I'll just put them there and leave.
Strange to have an unfinished inscription on the urn though. All it says is, ""YOU ARE""."});
	}
}