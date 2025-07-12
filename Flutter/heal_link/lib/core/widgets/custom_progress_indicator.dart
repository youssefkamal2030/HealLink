
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class CustomProgressBar extends StatelessWidget {
  final double progress; // from 0.0 to 1.0
  final Color indicatorColor;
 
  const CustomProgressBar({
    super.key,
    required this.progress,
    required this.indicatorColor,
    
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const SizedBox(height: 4),
        Row(
          children: [
            Expanded(
              child: LinearProgressIndicator(
                borderRadius: BorderRadius.circular(4),
                value: progress,
                minHeight: 8,
                backgroundColor: Colors.grey[300],
                valueColor: AlwaysStoppedAnimation<Color>(indicatorColor),
              ),
            ),
            const SizedBox(width: 8),
            Text(
              '${(progress * 100).toInt()}%',
              style: AppTextStyles.popins400style12LightBlackColor,
            ),
          ],
        ),
      ],
    );
  }
}
