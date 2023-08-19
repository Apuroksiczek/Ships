import { BoardModel } from '../models/BoardModel';

export default function Board({ board }: BoardModel) {
  return (
    <div className="board">
      {board.map((row, index) => (
        <div key={index} className="board-row">
          {row.map((item, itemIndex) => (
            <div key={itemIndex} className="board-cell">
              {item}
            </div>
          ))}
        </div>
      ))}
    </div>
  )
}
