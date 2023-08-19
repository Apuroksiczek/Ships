import axios from 'axios';
import { useEffect, useState } from 'react';
import { ShipsResponse } from '../models/ShipsResponse';
import Title from '../components/Title';
import BoardsContainer from '../components/BoardsContainer';
import ResetButton from '../components/ResetButton';

export default function MainPage() {
  const [data, setData] = useState<ShipsResponse | null>(null);
  const [loaded, setLoaded] = useState<boolean>(false);

  function getMovePlayer() {
    let colorClass = data?.playerOneMove ? "red" : "blue";

    return (
      <span className='main-page-content__player-move'>
        <span className={colorClass}>Player{data?.playerOneMove ? "1" : "2"} :</span> Move
      </span>)
  }

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await axios.get("https://localhost:7043/api/ships/game-status");
        setData(response.data);
      } catch (error: any) {
        console.log(error.message);
      } finally {
        setLoaded(true);
      }
    }

    fetchData();

    const interval = setInterval(fetchData, 10);

    return () => {
      clearInterval(interval);
    };

  }, [loaded]);

  async function reset() {
    try {
      await axios.put("https://localhost:7043/api/ships/reset");
    } catch (error: any) {
      console.log(error.message);
    } finally {
      setLoaded(false);
    }
  }

  return (
    <div className='main-page'>
      <Title
        text={data?.winner}
      />
      {data && (
        <div className='main-page-content'>
          <BoardsContainer
            playerOneBoard={data.playerOneBoard}
            playerTwoBoard={data.playerTwoBoard}
          />
          {getMovePlayer()}
          {data?.winner &&
            <ResetButton
              handleClick={reset}
            />
          }
        </div>
      )}
    </div>
  );
}
