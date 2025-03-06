import { Inject, Injectable, NotFoundException, UnauthorizedException } from '@nestjs/common';
import Redis from 'ioredis';
import { CreateRegistracionDto } from 'src/user/dto/create-registracion.dto';
import * as bcrypt from 'bcrypt';

@Injectable()
export class RedisRepository {
  constructor(@Inject('REDIS_CLIENT') private redisClient: Redis) { }


  async registerUserRedis(userDto: CreateRegistracionDto): Promise<any> {
    try {
      const { email, password, userName, iPadrres, deviceName } = userDto;

      if (!email || !password || !userName) {
        throw new Error('Email, password, and username are required');
      }
      let hashpassword = await bcrypt.hash(userDto.password, 10);
      console.log(hashpassword)
      await this.redisClient.hset(`user:${email}`, {
        userName,
        email,
        password: hashpassword,
        iPadrres: iPadrres || '',
        deviceName: deviceName || '',
      });

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

    //  console.log(`User ${email} logged in successfully`);


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

      console.log('findOneRedis',email)
        const userData = await this.redisClient.hgetall(`user:${email}`);

     
        if (!userData || Object.keys(userData).length === 0) {
            console.log(`User with email ${email} not found in Redis`);
            return null;
        }

        return {
            userName: userData.userName,
            email: userData.email,
            password: await bcrypt.hash(userData.password, 10),
            iPadrres: userData.iPadrres || null,
            deviceName: userData.deviceName || null,
        };
    } catch (error) {
        console.error('Error fetching user from Redis:', error);
        return null;
    }
}






  async updateUser(email: string, updatedData: Partial<CreateRegistracionDto>): Promise<string> {
    try {
      const existingUser = await this.findOneRedis(email);
      if (!existingUser) {
        return 'User not found';
      }


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
