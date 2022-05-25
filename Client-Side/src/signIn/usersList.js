import HarryContacts from "../Hard-coded-data/HarryContacts";
import QueenContacts from "../Hard-coded-data/QueenContacts";
import DonaldContacts from "../Hard-coded-data/DonaldContacts";
import SnowWhiteContacts from "../Hard-coded-data/SnowWhiteContacts";
import olofContacts from "../Hard-coded-data/olofContacts";
export const usersList = [{ username: 'harry', password: '1a', nickName:'Harry Potter', picture: "/harryPotter.jpg", contacts: HarryContacts},
{ username: 'queen', password: '1b', nickName:'Queen Elisabeth', picture: "/QueenElisabeth.jpg", contacts: QueenContacts},
{ username: 'donald', password: '1c',nickName:'Donald Trump', picture: "/donaldTrump.jpg", contacts: DonaldContacts}, 
{username: 'snow', password: '1d',nickName:'Snow white', picture: "/snowWhite.jpg", contacts: SnowWhiteContacts},
{username: 'olof', password: '1e',nickName:'olof snow man', picture: "/olof.jpg", contacts: olofContacts},
{username: '1', password: '12345', nickName:'A', picture: "/harryPotter.jpg", contacts: HarryContacts}];

export default usersList;