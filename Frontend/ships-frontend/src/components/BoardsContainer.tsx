import { BoardsContainerModel } from '../models/BoardsContainerModel'
import Board from './Board'

export default function BoardsContainer({ playerOneBoard, playerTwoBoard }: BoardsContainerModel) {
  return (
    <div className='boards'>
      <div className='boards-board'>
        <span className='boards-board__title'>Player 1 Board:</span>
        <Board board={playerOneBoard} />
      </div>
      <div className='boards-board'>
        <span className='boards-board__title'>Player 2 Board:</span>
        <Board board={playerTwoBoard} />
      </div>
    </div>
  )
}
