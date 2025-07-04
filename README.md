# Chest System
Replicating Chest System inspired by Clash Royale game.

## Design Patterns Used:
<ol>
  <li><b>MVC: </b> Used in <b>Player</b> and <b>Chest</b> mechanics.</li>
  <li><b>Observer Pattern: </b> 
    <ul>
      <li>Used for <b>data flow</b> from ChestView to ChestController.</li>
      <li>Used for chest <b>UI Updates</b> when chest's state chages.</li>
      <li>Used to <b>update currencies</b> (called from player)</li>
    </ul>
  </li>
  <li><b>Object Pooling:</b> Used in <b>getting chest</b>.</li>
  <li><b>Command Pattern:</b> Used for <b>UNDO</b> Mechanic.</li>
  <li><b>StateMachine:</b> Used to <b>change Chest state</b> ie. LOCKED, UNLOCKED, UNLOCKING & COLLECTED.</li>
  <li><b>Dependency Injection:</b> Used to <b>set services</b> in the scripts where corresponding services are required.</li>
</ol>

## UI Design
![Image](https://github.com/Imran1720/Chest_System/blob/60789748e9f7a4b047790e86511f73134b20dfc4/Design/Chest%20System%20UI.png)

## UML Diagram
https://miro.com/app/board/uXjVIl2xyMs=/?share_link_id=506075442137
