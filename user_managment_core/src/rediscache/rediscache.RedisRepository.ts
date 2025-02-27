import { Inject, Injectable, NotFoundException, UnauthorizedException } from '@nestjs/common';
import Redis from 'ioredis';
import { CreateRegistracionDto } from 'src/user/dto/create-registracion.dto';


@Injectable()
export class RedisRepository {
  constructor(@Inject('REDIS_CLIENT') private redisClient: Redis) { }


  async registerUserRedis(userDto: CreateRegistracionDto): Promise<any> {
    try {
      const { email, password, userName, iPadrres, deviceName } = userDto;

      if (!email || !password || !userName) {
        throw new Error('Email, password, and username are required');
      }


      await this.redisClient.hset(`user:${email}`, {
        userName,
        email,
        password,
        iPadrres: iPadrres || '',
        deviceName: deviceName || '',
      });

      console.log(`User registered with key user:${email}`);
    } catch (error) {
      console.error('Error storing user in Redis:', error);
    }
  }



  async loginRedis(email: string, password: string): Promise<CreateRegistracionDto> {
    try {
      if (!email || !password) {
        throw new UnauthorizedException('Email and password are required');
      }


      const userData = await this.redisClient.hgetall(`user:${email}`);

      if (!userData.email) {
        throw new UnauthorizedException('User not found');
      }


      if (userData.password !== password) {
        throw new UnauthorizedException('Invalid credentials');
      }

      console.log(`User ${email} logged in successfully`);


      return {
        userName: userData.userName,
        email: userData.email,
        password: userData.password,
        iPadrres: userData.iPadrres || null,
        deviceName: userData.deviceName || null,
      };
    } catch (error) {
      console.error('Login error:', error.message);
      throw error;
    }
  }




  async findOneRedis(email: string): Promise<CreateRegistracionDto | null> {
    try {

      const userData = await this.redisClient.hgetall(`user:${email}`);

      if (!userData.email) {
        console.log(`User with email ${email} not found`);
        return null;
      }

      console.log(`User found:`, userData);


      return {
        userName: userData.userName,
        email: userData.email,
        password: userData.password,
        iPadrres: userData.iPadrres || null,
        deviceName: userData.deviceName || null,
      };
    } catch (error) {
      console.error('Error fetching user from Redis:', error);
      return null;
    }
  }





  async updateUser(email: string, updatedData: Partial<CreateRegistracionDto>): Promise<void> {
    try {
      const existingUser = await this.findOneRedis(email);
      // if (!existingUser) {
      //     throw new NotFoundException('User not found');
      // }


      await this.redisClient.hset(`user:${email}`, {
        userName: updatedData.userName || existingUser.userName,
        password: updatedData.password || existingUser.password,
        iPadrres: updatedData.iPadrres || existingUser.iPadrres,
        deviceName: updatedData.deviceName || existingUser.deviceName,
      });

      console.log(`User ${email} updated successfully`);
    } catch (error) {
      console.error('Error updating user in Redis:', error);
    }
  }





  async deleteUser(email: string): Promise<any> {
    try {
      const deleted = await this.redisClient.del(`user:${email}`);

      if (deleted === 0) {
        return 'User not found';
      }

      console.log(`User ${email} deleted successfully`);
    } catch (error) {
      console.error('Error deleting user from Redis:', error);
    }
  }

  

  async findAllEntities(): Promise<{ key: string; value: string | null }[]> {
    let cursor = '0';
    let keys: string[] = [];

    do {
      const [nextCursor, foundKeys] = await this.redisClient.scan(cursor, 'COUNT', 100);
      cursor = nextCursor;
      keys.push(...foundKeys);
    } while (cursor !== '0');

    if (keys.length === 0) {
      return [];
    }

    const values = await this.redisClient.mget(...keys);

    return keys.map((key, index) => ({
      key,
      value: values[index],
    }));
}

}
